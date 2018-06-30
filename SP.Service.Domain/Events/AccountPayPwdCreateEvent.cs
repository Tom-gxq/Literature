using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccountPayPwdCreateEvent : Event
    {
        public string PayPwd { get; set; }

        public AccountPayPwdCreateEvent(string accountId, string payPwd)
        {
            base.AggregateId = new Guid(accountId);
            this.PayPwd = payPwd;
        }
    }
}
