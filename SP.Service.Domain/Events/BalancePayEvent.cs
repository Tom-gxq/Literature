using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class BalancePayEvent : Event
    {
        public string AccountId { get; set; }
        public string PassWord { get; set; }
        public string OrderCode { get; set; }
        public double Amount { get; set; }
        public string Sign { get; set; }

        public BalancePayEvent(Guid id, string accountId, string orderCode, string password,double amount,string sign)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.AccountId = accountId;
            this.PassWord = password;
            this.OrderCode = orderCode;
            this.Amount = amount;
            this.Sign = sign;
            this.EventType = EventType.BalancePay;
        }
    }
}
