using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class OrderProductCreatedEventHandler : IEventHandler<OrderProductCreatedEvent>
    {
        private readonly OrderReportDatabase _reportDatabase;
        public OrderProductCreatedEventHandler(OrderReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(OrderProductCreatedEvent handle)
        {
            var item = new OrdersEntity()
            {


            };

            _reportDatabase.Add(item);
        }
    }
}
