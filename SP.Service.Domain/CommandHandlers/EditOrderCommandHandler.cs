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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private ProductReportDatabase _productReportDatabase;
        private ShoppingCartReportDatabase _shoppingCartReportDatabase;

        public EditOrderCommandHandler(IDataRepository<OrderDomain> repository, IDataRepository<TradeDomain> tradeRepository,
            IDataRepository<AccountFinanceDomain> financeRepository, OrderReportDatabase orderReportDatabase, 
            IDataRepository<ProductSkuDomain> skuRepository, AccountFinanceReportDatabase financeReportDatabase,
            AddressReportDatabase addressReportDatabase, ShipOrderReportDatabase shipReportDatabase, ProductReportDatabase productReportDatabase,
            ShoppingCartReportDatabase shoppingCartReportDatabase)
        {
            this._repository = repository;
            this._tradeRepository = tradeRepository;
            this._financeRepository = financeRepository;
            this._orderReportDatabase = orderReportDatabase;
            this._skuRepository = skuRepository;
            this._financeReportDatabase = financeReportDatabase;
            this._addressReportDatabase = addressReportDatabase;
            this._shipReportDatabase = shipReportDatabase;
            this._productReportDatabase = productReportDatabase;
            this._shoppingCartReportDatabase = shoppingCartReportDatabase;
        }

        public void Execute(EditOrderCommand command)
        {
            var aggregate = new OrderDomain();
            aggregate.EditOrderDomainStatus(command.Id, command.OrderStatus, command.PayWay);
            _repository.Save(aggregate);
            
            var order = _orderReportDatabase.GetLeadOrderDomainByOrderId(command.Id.ToString());
            CaclCommsion(command.Id,order, command.OrderStatus);
        }

        public void Execute(EditOrderCodeCommand command)
        {
            var order = _orderReportDatabase.GetLeadOrderDomainByOrderCode(command.OrderCode);
            if (order != null)
            {
                var aggregate = new OrderDomain();
                aggregate.EditOrderDomainStatus(new Guid(order.OrderId), command.OrderStatus, command.PayWay);
                _repository.Save(aggregate);
                if(order.OrderStatus == OrderStatus.Closed && order.ShoppingCarts != null)
                {
                    foreach (var cart in order.ShoppingCarts)
                    {
                        var sku = new ProductSkuDomain();
                        sku.EditProductSkuDomainStock(command.Id, cart.ShopId, cart.Product.ProductId, cart.Quantity, order.OrderId, order.AccountId);
                        _skuRepository.Save(sku);
                    }
                }

                CaclCommsion(command.Id,order, command.OrderStatus);
            }
            else
            {
                throw new OrderCodeUpdateException(command.OrderCode);
            }
        }

        public void Execute(EditPurchaseOrderCommand command)
        {
            var aggregate = new OrderDomain();
            aggregate.EditShipOrderDomainStatus(command.Id, command.OrderStatus, command.PayWay, command.AccountId);
            _repository.Save(aggregate);

            var order = _orderReportDatabase.GetLeadOrderDomainByOrderId(command.Id.ToString());
            CaclSellerCommsion(command.Id,order, command.OrderStatus);
        }

        private void CaclSellerCommsion(Guid id,LeadOrderDomain order, OrderStatus orderStatus)
        {
            if (order != null)
            {
                var shipOrder = _shipReportDatabase.GetShippingOrdersByOrderId(order.OrderId);
                if (orderStatus == Data.Enum.OrderStatus.Success)
                {
                    if (shipOrder != null && shipOrder.Count > 0)
                    {
                        var finance = _financeReportDatabase.GetAccountFinanceDetail(shipOrder[0].ShippingId);
                        if (finance != null && !string.IsNullOrEmpty(finance.AccountId))
                        {
                            var financeDomain = new AccountFinanceDomain();
                            financeDomain.EditHaveAmount(id,finance.AccountId, order.Amount);
                            _financeRepository.Save(financeDomain);
                        }
                        else
                        {
                            var financeDomain = new AccountFinanceDomain(shipOrder[0].ShippingId, order.Amount);
                            _financeRepository.Save(financeDomain);
                        }
                    }
                }
                else if (orderStatus == Data.Enum.OrderStatus.Payed)
                {
                    var list = shipOrder.GroupBy(x=>x.ShippingId);
                    foreach (var item in list)
                    {                        
                        double sumOrderAmount = 0;
                        foreach (var ship in item)
                        {
                            var shopCart = _shoppingCartReportDatabase.GetShoppingCartByOrderIdandProductId(ship.OrderId, ship.ProductId);
                            if (shopCart != null)
                            {
                                sumOrderAmount += shopCart.Quantity * shopCart.Product.PurchasePrice.Value;
                                System.Console.WriteLine($"CaclSellerCommsion Stock={shopCart.Quantity}  PurchasePrice={shopCart.Product.PurchasePrice.Value}");
                            }
                        }
                        //将订单成功付款的信息添加到kafka队列中
                        AddShipOrderKafka(id,order.OrderId, orderStatus, item.Key, sumOrderAmount, item.FirstOrDefault()?.ShipTo??string.Empty);
                    }
                }
            }
        }
        private void CaclCommsion(Guid id,LeadOrderDomain order, OrderStatus orderStatus)
        {
            if (order != null && orderStatus == Data.Enum.OrderStatus.Payed)
            {
                var list = _shipReportDatabase.GetShippingOrdersByOrderId(order.OrderId);
                foreach (var ship in list)
                {
                    //更新产品已卖出的数量
                    if (ship != null && ship.Id > 0)
                    {
                        System.Console.WriteLine("Payed EditProductSkuDomainStock Quantity=" + ship.Stock);
                        var sku = new ProductSkuDomain();
                        //sku.EditProductSkuDomainStock(cart.Product.ProductId, cart.Quantity);
                        sku.EditProductSkuOrderNum(id,ship.ShopId.Value, ship.ProductId, ship.ShippingId, ship.Stock.Value);
                        _skuRepository.Save(sku);
                    }
                }
                //将订单成功付款的信息添加到kafka队列中
                AddKafka(id,order.OrderId, orderStatus);
            }
        }
        private void AddKafka(Guid id,string orderId,OrderStatus orderStatus)
        {
            var aggregate = _orderReportDatabase.GetOrderByOrderId(orderId);
            var address = _addressReportDatabase.GetDefaultSelectedAddress(aggregate.AccountId);
            if (address.SchoolId != 1)
            {
                System.Console.WriteLine($"EditOrder AddKafka OrderId={orderId}  AccountId={aggregate.AccountId}  SchoolId={address.SchoolId}");
            }
            else
            {
                System.Console.WriteLine($"EditOrder AddKafka OrderId={orderId}");
            }
            aggregate.AdressId = address.DormId;
            aggregate.AddKafkaInfo(id,orderStatus, address.SchoolId);
            _repository.Save(aggregate);
        }
        private void AddShipOrderKafka(Guid id,string orderId, OrderStatus orderStatus,string shippingId,double sumAmount, string shipto)
        {
            var aggregate = _orderReportDatabase.GetOrderByOrderId(orderId);
            aggregate.AddShipOrderKafka(id,orderStatus, shippingId, sumAmount,orderId, shipto);
            _repository.Save(aggregate);
        }
    }
}
