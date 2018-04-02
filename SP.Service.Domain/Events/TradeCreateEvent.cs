using Grpc.Service.Core.Domain.Events;
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
        public TradeCreateEvent(Guid aggregateId, string accountId, string cartId,int subject, double amount)
        {
            AggregateId = aggregateId;
            AccountId = accountId;
            CartId = cartId;
            Subject = subject;
            Amount = amount;
        }
    }
}
