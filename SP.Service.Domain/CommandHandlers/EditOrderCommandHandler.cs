using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using Microsoft.Extensions.Configuration;
using SP.Data.Enum;
using SP.Service.Domain.Commands.Order;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditOrderCommandHandler : ICommandHandler<EditOrderCommand>,
        ICommandHandler<EditOrderCodeCommand>,ICommandHandler<EditPurchaseOrderCommand>
    {
        private IDataRepository<OrderDomain> _repository;
        private IDataRepository<TradeDomain> _tradeRepository;
        private IDataRepository<AccountFinanceDomain> _financeRepository;
        private IDataRepository<ProductSkuDomain> _skuRepository;
        private OrderReportDatabase _orderReportDatabase;
        private AccountFinanceReportDatabase _financeReportDatabase;
        private AddressReportDatabase _addressReportDatabase;
        private ShipOrderReportDatabase _shipReportDatabase;

        public EditOrderCommandHandler(IDataRepository<OrderDomain> repository, IDataRepository<TradeDomain> tradeRepository,
            IDataRepository<AccountFinanceDomain> financeRepository, OrderReportDatabase orderReportDatabase, 
            IDataRepository<ProductSkuDomain> skuRepository, AccountFinanceReportDatabase financeReportDatabase,
            AddressReportDatabase addressReportDatabase, ShipOrderReportDatabase shipReportDatabase)
        {
            this._repository = repository;
            this._tradeRepository = tradeRepository;
            this._financeRepository = financeRepository;
            this._orderReportDatabase = orderReportDatabase;
            this._skuRepository = skuRepository;
            this._financeReportDatabase = financeReportDatabase;
            this._addressReportDatabase = addressReportDatabase;
            this._shipReportDatabase = shipReportDatabase;
        }

        public void Execute(EditOrderCommand command)
        {
            var aggregate = new OrderDomain();
            aggregate.EditOrderDomainStatus(command.Id, command.OrderStatus, command.PayWay);
            _repository.Save(aggregate);
            
            var order = _orderReportDatabase.GetLeadOrderDomainByOrderId(command.Id.ToString());
            CaclCommsion(order, command.OrderStatus);
        }

        public void Execute(EditOrderCodeCommand command)
        {
            var order = _orderReportDatabase.GetLeadOrderDomainByOrderCode(command.OrderCode);
            if (order != null)
            {
                var aggregate = new OrderDomain();
                aggregate.EditOrderDomainStatus(new Guid(order.OrderId), command.OrderStatus, command.PayWay);
                _repository.Save(aggregate);

                CaclCommsion(order, command.OrderStatus);
            }
            else
            {
                throw new OrderCodeUpdateException(command.OrderCode);
            }
        }

        public void Execute(EditPurchaseOrderCommand command)
        {
            var aggregate = new OrderDomain();
            aggregate.EditShipOrderDomainStatus(command.Id, command.OrderStatus, command.PayWay);
            _repository.Save(aggregate);

            var order = _orderReportDatabase.GetLeadOrderDomainByOrderId(command.Id.ToString());
            CaclCommsion(order, command.OrderStatus);
        }

        private void CaclCommsion(LeadOrderDomain order, OrderStatus orderStatus)
        {
            double commsion = 0;
            if (order != null && order.Shop != null && !string.IsNullOrEmpty(order.Shop.OwnerId)
                && order.ShoppingCarts != null)
            {
                
                if (orderStatus == Data.Enum.OrderStatus.Success)
                {
                    var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                    int marketId = -1;
                    if (config != null)
                    {
                        var reObj = config.GetSection("MarketId");
                        int.TryParse(reObj?.Value,out marketId);
                    }
                    foreach (var cart in order.ShoppingCarts)
                    {
                        if (cart != null && !string.IsNullOrEmpty(cart.CartId))
                        {
                            double amount = 0;
                            if (marketId == order.Shop.ShopType)
                            {
                                //超市提成方法
                                amount = order.IsVip ? cart.VIPAmount: cart.Amount;
                            }
                            else
                            {
                                //餐饮提成方法
                                amount = cart.Quantity * 1;
                            } 
                            var trade = new TradeDomain(order.Shop.OwnerId, cart.CartId, 1, amount);
                            _tradeRepository.Save(trade);
                            commsion += amount;
                        }
                    }
                    var finance = _financeReportDatabase.GetAccountFinanceDetail(order.Shop.OwnerId);
                    if (finance != null && !string.IsNullOrEmpty(finance.AccountId))
                    {
                        var financeDomain = new AccountFinanceDomain();
                        financeDomain.EditHaveAmount(finance.AccountId, commsion);
                        _financeRepository.Save(financeDomain);
                    }
                    else
                    {
                        var financeDomain = new AccountFinanceDomain(order.Shop.OwnerId, commsion);
                        _financeRepository.Save(financeDomain);
                    }
                }
                else if (orderStatus == Data.Enum.OrderStatus.Payed)
                {
                    var list = _shipReportDatabase.GetShippingOrdersByOrderId(order.OrderId);
                    foreach (var ship in list)
                    {
                        if (ship != null && ship.Id > 0)
                        {
                            System.Console.WriteLine("Payed EditProductSkuDomainStock Quantity=" + ship.Stock);
                            var sku = new ProductSkuDomain();
                            //sku.EditProductSkuDomainStock(cart.Product.ProductId, cart.Quantity);
                            sku.EditProductSkuOrderNum(ship.ShopId.Value, ship.ProductId, ship.ShippingId, ship.Stock.Value);
                            _skuRepository.Save(sku);
                        }
                    }
                    //将订单成功付款的信息添加到kafka队列中
                    AddKafka(order.OrderId,orderStatus);
                }
            }
        }
        private void AddKafka(string orderId,OrderStatus orderStatus)
        {
            var aggregate = _orderReportDatabase.GetOrderByOrderId(orderId);
            var address = _addressReportDatabase.GetRegionData(aggregate.AdressId);
            aggregate.AddKafkaInfo(orderStatus, address.ParentDataID);
            _repository.Save(aggregate);
        }
    }
}
