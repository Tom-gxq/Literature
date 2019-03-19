using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Events;

namespace SP.Service.Domain.Events
{
    public class IncomeTradeCreateEvent : TradeBaseEvent
    {
        public string TradeId { get; set; }
        public string ProductId { get; set; }
        public int Subject { get; set; }
        public double Amount { get; set; }
        public int ShipOrderId { get; set; }
        public IncomeTradeCreateEvent(Guid id, string accountId, string tradeNo, double balanceAmount,
            double amount, int subject, int shipOrderId, string productId)
            : base(id, accountId, tradeNo, balanceAmount)
        {
            this.AggregateId = id;
            this.CommandId = id.ToString();
            this.Subject = subject;
            this.Amount = amount;
            this.ShipOrderId = shipOrderId;
            this.ProductId = productId;
            this.EventType = EventType.IncomeTradeCreate;
        }
    }
}
