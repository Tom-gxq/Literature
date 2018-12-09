using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccountInfoCreatedEvent : Event
    {
        public string Fullname { get;  set; }
        public string Avatar { get;  set; }
        public string Email { get;  set; }
        public string WeiXin { get;  set; }
        public int UserType { get;  set; }
        public bool Gender { get;  set; }
        public DateTime Birthdate { get;  set; }
        public string IM_QQ { get;  set; }

        public AccountInfoCreatedEvent(Guid aggregateId, string avatar="", string fullname="", string weiXin="", string imqq="", int userType = 0, bool gender = false, DateTime? birthdate = null)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            CommandId = aggregateId.ToString();
            Avatar = Avatar;
            Fullname = fullname;
            WeiXin = weiXin;
            UserType = userType;
            Gender = gender;
            Birthdate = birthdate != null ? birthdate.Value:DateTime.MinValue;
            IM_QQ = imqq;
            EventType = EventType.AccountInfoCreated;
        }
    }
}
