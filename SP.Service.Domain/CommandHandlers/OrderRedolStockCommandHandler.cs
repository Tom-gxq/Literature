using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Order;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class OrderRedolStockCommandHandler : ICommandHandler<OrderRedoStockCommand>
    {
        private IDataRepository<OrderDomain> _repository;
        private IDataRepository<ProductSkuDomain> _skuRepository;
        private OrderReportDatabase _orderReportDatabase;
        private ShipOrderReportDatabase _shipOrderReportDatabase;
        public OrderRedolStockCommandHandler(IDataRepository<OrderDomain> repository,
            IDataRepository<ProductSkuDomain> skuRepository,OrderReportDatabase orderReportDatabase,
            ShipOrderReportDatabase shipOrderReportDatabase)
        {
            this._repository = repository;
            this._skuRepository = skuRepository;
            this._orderReportDatabase = orderReportDatabase;
            this._shipOrderReportDatabase = shipOrderReportDatabase;
        }

        public void Execute(OrderRedoStockCommand command)
        {
            var orderDomain = _orderReportDatabase.GetOrderByOrderId(command.Id.ToString());
            
            if ((orderDomain != null)&&(orderDomain.OrderStatus ==  Data.Enum.OrderStatus.WaitPay))
            {
                var carts = _shipOrderReportDatabase.GetShippingOrdersByOrderId(command.Id.ToString());
                foreach (var cart in carts)
                {
                    if (cart != null && !string.IsNullOrEmpty(cart.ShippingId) && !string.IsNullOrEmpty(cart.ProductId))
                    {
                        System.Console.WriteLine("RedoProductSkuDomainStock OrderId="+ orderDomain.OrderId + "  ProductId="+ cart.ProductId + "  Quantity=" + cart.Stock+ " OrderDate=" + orderDomain.OrderDate.ToString("yyyy-MM-dd HH:mm:ss"));
                        var sku = new ProductSkuDomain();
                        sku.RedoProductSkuDomainStock(cart.ShopId.Value,cart.ProductId, cart.Stock.Value, cart.OrderId,cart.ShippingId);
                        _skuRepository.Save(sku);
                    }
                }
                orderDomain.EditOrderDomainStatus(command.Id, Data.Enum.OrderStatus.Closed, Data.Enum.OrderPay.None);
                _repository.Save(orderDomain);
            }            
        }
    }
}
