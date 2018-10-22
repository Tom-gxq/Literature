using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using Microsoft.Extensions.Configuration;
using SP.Data.Enum;
using SP.Service.Domain.Commands.StockShip;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreatShipOrderCommandHandler : ICommandHandler<CreatShipOrderCommand>,ICommandHandler<EditShipOrderStatusCommand>
    {
        private IDataRepository<ShipOrderDomain> _repository;
        private IDataRepository<TradeDomain> _tradeRepository;
        private IDataRepository<AccountFinanceDomain> _financeRepository;
        private IDataRepository<ProductSkuDomain> _skuRepository;
        private OrderReportDatabase _orderReportDatabase;
        private AccountFinanceReportDatabase _financeReportDatabase;
        private AddressReportDatabase _addressReportDatabase;
        private ShipOrderReportDatabase _shipReportDatabase;
        private ProductReportDatabase _productReportDatabase;
        private ShopReportDatabase _shopReportDatabase;
        private ShoppingCartReportDatabase _shoppingCartReportDatabase;
        private TradeReportDatabase _tradeReportDatabase;
        private static object lockObj = new object();
        private static object lockSecondObj = new object();

        public CreatShipOrderCommandHandler(IDataRepository<ShipOrderDomain> repository, IDataRepository<TradeDomain> tradeRepository,
            IDataRepository<AccountFinanceDomain> financeRepository, IDataRepository<ProductSkuDomain> skuRepository,
            AccountFinanceReportDatabase financeReportDatabase,OrderReportDatabase orderReportDatabase,
            AddressReportDatabase addressReportDatabase, ShipOrderReportDatabase shipReportDatabase,
            ProductReportDatabase productReportDatabase, ShopReportDatabase shopReportDatabase,
            ShoppingCartReportDatabase shoppingCartReportDatabase, TradeReportDatabase tradeReportDatabase)
        {
            this._repository = repository;
            this._tradeRepository = tradeRepository;
            this._financeRepository = financeRepository;
            this._skuRepository = skuRepository;
            this._financeReportDatabase = financeReportDatabase;
            this._orderReportDatabase = orderReportDatabase;
            this._addressReportDatabase = addressReportDatabase;
            this._shipReportDatabase = shipReportDatabase;
            this._productReportDatabase = productReportDatabase;
            this._shopReportDatabase = shopReportDatabase;
            this._shoppingCartReportDatabase = shoppingCartReportDatabase;
            this._tradeReportDatabase = tradeReportDatabase;
        }

        public void Execute(CreatShipOrderCommand command)
        {
            var aggregate = new ShipOrderDomain(command.OrderId, command.ShippingId,command.ShipTo, command.ShippingDate, command.Stock, command.ProductId, command.ShopId);
            
            _repository.Save(aggregate);
        }

        public void Execute(EditShipOrderStatusCommand command)
        {
            var aggregate = new ShipOrderDomain();
            aggregate.EditShipOrderDomainStatus(command.ShipOrderId, (command.OrderStatus== OrderStatus.Success));
            _repository.Save(aggregate);
            CaclCommsion(command.ShipOrderId, command.OrderStatus);
        }

        private void CaclCommsion(List<int> shipOrder, OrderStatus orderStatus)
        {
            double commsion = 0;
            if (shipOrder != null && shipOrder.Count > 0)
            {
                var shipEntity = _shipReportDatabase.GetShippingOrdersById(shipOrder[0]);
                var order = _orderReportDatabase.GetLeadOrderDomainByOrderId(shipEntity.OrderId);
                if (orderStatus == Data.Enum.OrderStatus.Success)
                {
                    var config = IocManager.Instance.Resolve<IConfigurationRoot>();
                    int marketId = -1;
                    if (config != null)
                    {
                        var reObj = config.GetSection("MarketId");
                        int.TryParse(reObj?.Value, out marketId);
                    }
                    foreach (var ship in shipOrder)
                    {
                        var tradeHis = _tradeReportDatabase.GetTradeByShipOrderId(ship);
                        if (tradeHis == null)
                        {
                            System.Console.WriteLine("EditShipOrderStatusCommand ship="+ ship);
                            var shipOrderEntity = _shipReportDatabase.GetShippingOrdersById(ship);
                            if (shipOrderEntity != null)
                            {
                                var shopCart = _shoppingCartReportDatabase.GetShoppingCartByOrderIdandProductId(shipOrderEntity.OrderId, shipOrderEntity.ProductId);
                                if (shopCart != null)
                                {
                                    double amount = 0;
                                    ShopDomain shopDomain = null;
                                    if (shipOrderEntity.ShopId != null && shipOrderEntity.ShopId > 0)
                                    {
                                        shopDomain = this._shopReportDatabase.GetShopById(shipOrderEntity.ShopId.Value);
                                    }

                                    if (marketId == (shopDomain?.ShopType ?? marketId))
                                    {
                                        //超市提成方法
                                        amount = order.IsVip ? shopCart.Product.VIPPrice.Value : shopCart.Product.MarketPrice.Value;
                                        //购买商品的数量
                                        amount = amount * (shipOrderEntity.Stock != null ? shipOrderEntity.Stock.Value : 1);
                                    }
                                    else
                                    {
                                        //餐饮提成方法
                                        amount = shipOrderEntity.Stock.Value * 1;
                                    }
                                    var trade = new TradeDomain(shipOrderEntity.ShippingId, shopCart.CartId, 1, amount, ship);
                                    _tradeRepository.Save(trade);
                                    commsion += amount;
                                }
                            }
                        }
                    }

                    var finance = _financeReportDatabase.GetAccountFinanceDetail(shipEntity.ShippingId);
                    if (finance != null && !string.IsNullOrEmpty(finance.AccountId))
                    {
                        var financeDomain = new AccountFinanceDomain();
                        financeDomain.EditHaveAmount(finance.AccountId, commsion);
                        _financeRepository.Save(financeDomain);
                    }
                    else
                    {
                        var financeDomain = new AccountFinanceDomain(shipEntity.ShippingId, commsion);
                        _financeRepository.Save(financeDomain);
                    }
                }
                else if (orderStatus == Data.Enum.OrderStatus.Payed)
                {
                    foreach (var ship in shipOrder)
                    {
                        var shipOrderEntity = _shipReportDatabase.GetShippingOrdersById(ship);
                        if (shipOrderEntity != null)
                        {
                            System.Console.WriteLine("Payed EditProductSkuDomainStock Quantity=" + shipOrderEntity.Stock);
                            var sku = new ProductSkuDomain();
                            //sku.EditProductSkuDomainStock(cart.Product.ProductId, cart.Quantity);
                            sku.EditProductSkuOrderNum(shipOrderEntity.ShopId.Value, shipOrderEntity.ProductId, shipOrderEntity.ShippingId, shipOrderEntity.Stock.Value);
                            _skuRepository.Save(sku);
                        }
                    }
                    //将订单成功付款的信息添加到kafka队列中
                    //AddKafka(order.OrderId, orderStatus);
                }
            }
        }
        private async void AddKafka(string orderId, OrderStatus orderStatus)
        {
            var aggregate = _orderReportDatabase.GetOrderByOrderId(orderId);
            var address = _addressReportDatabase.GetDefaultSelectedAddress(aggregate.AccountId);
            if (address.SchoolId != 1)
            {
                System.Console.WriteLine($"CreatShipOrder AddKafka OrderId={orderId}  AccountId={aggregate.AccountId}  SchoolId={address.SchoolId}");
            }
            else
            {
                System.Console.WriteLine($"EditOrder AddKafka OrderId={orderId}");
            }
            aggregate.AddKafkaInfo(orderStatus, address.SchoolId);
            _repository.Save(aggregate);
        }
    }
}
