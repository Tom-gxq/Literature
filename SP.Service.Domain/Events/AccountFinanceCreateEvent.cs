using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccountFinanceCreateEvent : Event
    {
        public string AccountId { get; internal set; }
        public double Amount { get; set; }
        public AccountFinanceCreateEvent(string accountId,double haveAmount)
        {
            AccountId = accountId;
            Amount = haveAmount;
        }
    }
}
