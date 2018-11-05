using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class HaveAmountEditEvent : Event
    {
        public string AccountId { get; set; }
        public double Amount { get; set; }
        public HaveAmountEditEvent(string accountId,double amount) : base(KafkaConfig.EventBusTopicTitle)
        {
            AccountId = accountId;
            Amount = amount;
            EventType = EventType.HaveAmountEdit;
        }
    }
}
