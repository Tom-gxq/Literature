﻿using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Events;
using SP.Data.Enum;
using SP.Service.Domain.Util;

namespace SP.Service.Domain.Events
{
    public class AccountCreatedEvent : Event
    {
        public string MobilePhone { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public int Status { get;  set; }
        public string WxUnionId { get; set; }
        public string WxBind { get; set; }
        public int AccountType { get; set; }

        public AccountCreatedEvent(Guid aggregateId, string mobilePhone, string email,string password,
            int status,string wxUnionId="",string wxBind="",int accountType=0) : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            CommandId = aggregateId.ToString();
            MobilePhone = mobilePhone;
            Email = email;
            Password = password;
            Status = status;
            WxUnionId = wxUnionId;
            WxBind = wxBind;
            AccountType = accountType;
            EventType = EventType.AccountCreated;
        }
    }
    public class AliBindCreatedEvent :AccountCreatedEvent
    {
        public string OtherAccount { get; set; }
        public AliBindCreatedEvent(Guid aggregateId, string mobilePhone, string otherAccount) :base(aggregateId, mobilePhone,null,null,1)
        {
            this.OtherAccount = otherAccount;
            this.EventType = EventType.AliBindCreated;
        }
    }
    public class WxBindCreatedEvent : AccountCreatedEvent
    {
        public string OtherAccount { get; set; }
        public WxBindCreatedEvent(Guid aggregateId, string mobilePhone, string otherAccount) : base(aggregateId, mobilePhone, null, null, 1)
        {
            this.OtherAccount = otherAccount;
            this.EventType = EventType.WxBindCreated;
        }
    }
    public class QQBindCreatedEvent : AccountCreatedEvent
    {
        public string OtherAccount { get; set; }
        public QQBindCreatedEvent(Guid aggregateId, string mobilePhone, string otherAccount) : base(aggregateId, mobilePhone, null, null, 1)
        {
            this.OtherAccount = otherAccount;
            this.EventType = EventType.QQBindCreated;
        }
    }
}
