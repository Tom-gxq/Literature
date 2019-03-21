using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Events;

namespace SP.Service.Domain.Events
{
    public class ConsumeTradeCreateEvent : TradeBaseEvent
    {
        public string TradeId { get; set; }
        public double Amount { get; set; }
        public string OrderId { get; set; }
        public string CheckCode { get; set; }

        public ConsumeTradeCreateEvent(Guid id, string accountId, string tradeNo, double balanceAmount,
             double amount, string orderId, string sign)
            : base(id, accountId, tradeNo, balanceAmount)
        {
            this.AggregateId = id;
            this.CommandId = id.ToString();
            this.Amount = amount;
            this.OrderId = orderId;
            this.CheckCode = sign;
            this.EventType = EventType.ConsumeTradeCreate;
        }
    }
}
