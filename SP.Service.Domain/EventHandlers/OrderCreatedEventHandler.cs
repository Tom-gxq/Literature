using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class OrderCreatedEventHandler : IEventHandler<OrderCreatedEvent>
    {
        private readonly OrderReportDatabase _reportDatabase;
        public OrderCreatedEventHandler(OrderReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(OrderCreatedEvent handle)
        {
            var item = new OrdersEntity()
            {
                OrderId = handle.AggregateId.ToString(),
                Amount = handle.Amount,
                VIPAmount = handle.VIPAmount,
                AccountId = handle.AccountId,
                OrderDate =handle.OrderDate,
                OrderStatus = (int)handle.OrderStatus,
                OrderCode = DateTime.Now.ToString("yyyyMMddHH24mmssffff"),
                AddressId = handle.AddressId,
                OrderAddress = handle.Address
            };

            _reportDatabase.Add(item);
        }
    }
}
