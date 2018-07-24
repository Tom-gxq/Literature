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
            var orderCode = DateTime.Now.ToString("yyyyMMddHH24mmssffff");
            var item = new OrdersEntity()
            {
                OrderId = handle.AggregateId.ToString(),
                Amount = handle.Amount,
                VIPAmount = handle.VIPAmount,
                AccountId = handle.AccountId,
                OrderDate = handle.OrderDate,
                OrderStatus = (int)handle.OrderStatus,
                OrderCode = orderCode,
                AddressId = handle.AddressId,
                OrderAddress = handle.Address,
                Meta_Keywords = $"{orderCode}|{handle.Mobile}",
                IsVip = handle.IsVip,
                IsAliPay = false,
                IsWxPay = false,
                OrderType = handle.OrderType

            };

            _reportDatabase.Add(item);
        }
    }
}
