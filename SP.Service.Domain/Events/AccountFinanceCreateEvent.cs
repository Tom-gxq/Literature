using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccountFinanceCreateEvent : Event
    {
        public string AccountId { get;  set; }
        public double Amount { get; set; }
        public AccountFinanceCreateEvent(string accountId,double haveAmount) : base(KafkaConfig.EventBusTopicTitle)
        {
            AccountId = accountId;
            Amount = haveAmount;
            EventType = EventType.AccountFinanceCreate;
        }
    }
}
