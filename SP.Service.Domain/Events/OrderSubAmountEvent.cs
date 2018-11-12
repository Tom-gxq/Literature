using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class OrderSubAmountEvent : Event
    {
        public string ProductId { get; set; }
        public double Amount { get; set; }
        public double VipAmount { get; set; }
        public OrderSubAmountEvent(Guid aggregateId,string productId, double amount, double vipAmount)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            ProductId = productId;
            Amount = amount;
            VipAmount = vipAmount;
            EventType = EventType.OrderSubAmount;
        }
    }
}
