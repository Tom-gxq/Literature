using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class CashApplyCreatedEvent : Event
    {
        public string AccountId { get; internal set; }
        public string Alipay { get; internal set; }
        public double Money { get; internal set; }
        public CashApplyCreatedEvent(string accountId, string alipay, double money)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.AccountId = accountId;
            this.Alipay = alipay;
            this.Money = money;
            this.EventType = EventType.CashApplyCreated;
        }
    }
}
