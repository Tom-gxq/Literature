using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class OrderSubAmountEvent : Event
    {
        public string ProductId { get; internal set; }
        public double Amount { get; internal set; }
        public double VipAmount { get; internal set; }
        public OrderSubAmountEvent(Guid aggregateId,string productId, double amount, double vipAmount)
        {
            AggregateId = aggregateId;
            ProductId = productId;
            Amount = amount;
            VipAmount = vipAmount;
        }
    }
}
