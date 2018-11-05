using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class TradeCreateEvent : Event
    {
        public string AccountId { get; set; }
        public string CartId { get; set; }
        public int Subject { get; set; }
        public double Amount { get; set; }
        public int ShipOrderId { get;  set; }
        public TradeCreateEvent(Guid aggregateId, string accountId, string cartId,int subject, double amount, int shipOrderId)
             : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            AccountId = accountId;
            CartId = cartId;
            Subject = subject;
            Amount = amount;
            ShipOrderId = shipOrderId;
            EventType = EventType.TradeCreate;
        }
    }
}
