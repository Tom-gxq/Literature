using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class ShipOrderCreatedEventHandler: IEventHandler<ShipOrderCreatedEvent>
    {
        private readonly ShipOrderReportDatabase _reportDatabase;
        public ShipOrderCreatedEventHandler(ShipOrderReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(ShipOrderCreatedEvent handle)
        {
            var item = new ShippingOrdersEntity()
            {
                OrderId = handle.OrderId,
                ProductId = handle.ProductId,
                ShippingId = handle.ShippingId,
                ShipTo = handle.ShipTo,
                Stock = handle.Stock,
                ShippingDate = handle.ShippingDate,
                ShopId = handle.ShopId,
                IsShipped = false,
            };

            _reportDatabase.Add(item);
        }
    }
}
