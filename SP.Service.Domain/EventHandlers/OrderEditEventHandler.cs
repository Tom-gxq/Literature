using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class OrderEditEventHandler : IEventHandler<OrderEditEvent>, IEventHandler<OrderSubAmountEvent>
    {
        private readonly OrderReportDatabase _reportDatabase;
        public OrderEditEventHandler(OrderReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(OrderEditEvent handle)
        {
            var item = new OrdersEntity()
            {
                OrderId = handle.AggregateId.ToString(),
                OrderStatus = (int)handle.OrderStatus
            };

            _reportDatabase.UpdateOrderStatus(item);
        }
        public void Handle(OrderSubAmountEvent handle)
        {
            var item = new OrdersEntity()
            {
                OrderId = handle.AggregateId.ToString(),
                Amount = handle.Amount,
                VIPAmount = handle.VipAmount
            };

            _reportDatabase.UpdateOrderStatus(item);
        }
    }
}
