using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Events;
using SP.Data.Enum;

namespace SP.Service.Domain.Events
{
    public class AccountCreatedEvent : Event
    {
        public string MobilePhone { get; internal set; }
        public string Email { get; internal set; }
        public string Password { get; internal set; }
        public int Status { get; internal set; }        

        public AccountCreatedEvent(Guid aggregateId, string mobilePhone, string email,string password,int status)
        {
            AggregateId = aggregateId;
            MobilePhone = mobilePhone;
            Email = email;
            Password = password;
            Status = status;
        }
    }
    public class AliBindCreatedEvent :AccountCreatedEvent
    {
        public string OtherAccount { get; set; }
        public AliBindCreatedEvent(Guid aggregateId, string mobilePhone, string otherAccount) :base(aggregateId, mobilePhone,null,null,1)
        {
            this.OtherAccount = otherAccount;
        }
    }
    public class WxBindCreatedEvent : AccountCreatedEvent
    {
        public string OtherAccount { get; set; }
        public WxBindCreatedEvent(Guid aggregateId, string mobilePhone, string otherAccount) : base(aggregateId, mobilePhone, null, null, 1)
        {
            this.OtherAccount = otherAccount;
        }
    }
    public class QQBindCreatedEvent : AccountCreatedEvent
    {
        public string OtherAccount { get; set; }
        public QQBindCreatedEvent(Guid aggregateId, string mobilePhone, string otherAccount) : base(aggregateId, mobilePhone, null, null, 1)
        {
            this.OtherAccount = otherAccount;
        }
    }
}
