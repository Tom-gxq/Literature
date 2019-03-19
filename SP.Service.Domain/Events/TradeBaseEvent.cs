using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class TradeBaseEvent : Event
    {
        public string AccountId { get; set; }
        public double BalanceAmount { get; set; }
        public string TradeNo { get; set; }
        public TradeBaseEvent(Guid id, string accountId, string tradeNo, double balanceAmount)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.AggregateId = id;
            this.CommandId = id.ToString();
            this.AccountId = accountId;
            this.TradeNo = tradeNo;
            this.BalanceAmount = balanceAmount;
        }
    }
}
