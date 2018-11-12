using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class CashApplyCreatedEvent : Event
    {
        public string AccountId { get;  set; }
        public string Alipay { get; set; }
        public double Money { get; set; }
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
