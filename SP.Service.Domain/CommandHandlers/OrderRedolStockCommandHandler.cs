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
        public OrderRedolStockCommandHandler(IDataRepository<OrderDomain> repository,
            IDataRepository<ProductSkuDomain> skuRepository,OrderReportDatabase orderReportDatabase)
        {
            this._repository = repository;
            this._skuRepository = skuRepository;
            _orderReportDatabase = orderReportDatabase;
        }

        public void Execute(OrderRedoStockCommand command)
        {
            var orderDomain = _orderReportDatabase.GetOrderByOrderId(command.Id.ToString());
            
            if ((orderDomain != null)&&(orderDomain.OrderStatus ==  Data.Enum.OrderStatus.WaitPay))
            {
                var carts = _orderReportDatabase.GetShoppingCartsByOrderId(command.Id.ToString());
                foreach (var cart in carts)
                {
                    if (cart != null && !string.IsNullOrEmpty(cart.CartId))
                    {
                        System.Console.WriteLine("RedoProductSkuDomainStock ProductId="+ cart.ProductId + "  Quantity=" + cart.Quantity);
                        var sku = new ProductSkuDomain();
                        sku.RedoProductSkuDomainStock(cart.ProductId, (cart.Quantity!=null? cart.Quantity.Value:0));
                        _skuRepository.Save(sku);
                    }
                }
                orderDomain.EditOrderDomainStatus(command.Id, Data.Enum.OrderStatus.Closed);
                _repository.Save(orderDomain);
            }            
        }
    }
}
