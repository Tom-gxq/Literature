using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class UseAmountEditEvent : Event
    {
        public string AccountId { get; set; }
        public double Amount { get; set; }
        public UseAmountEditEvent(string accountId, double amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
