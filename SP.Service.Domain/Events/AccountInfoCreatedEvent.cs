using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccountInfoCreatedEvent : Event
    {
        public string Fullname { get; internal set; }
        public string Avatar { get; internal set; }
        public string Email { get; internal set; }
        public string WeiXin { get; internal set; }
        public int UserType { get; internal set; }
        public bool Gender { get; internal set; }
        public DateTime Birthdate { get; internal set; }
        public string IM_QQ { get; internal set; }

        public AccountInfoCreatedEvent(Guid aggregateId, string avatar="", string fullname="", string weiXin="", string imqq="", int userType = 0, bool gender = false, DateTime? birthdate = null)
        {
            AggregateId = aggregateId;
            Avatar = Avatar;
            Fullname = fullname;
            WeiXin = weiXin;
            UserType = userType;
            Gender = gender;
            Birthdate = birthdate != null ? birthdate.Value:DateTime.MinValue;
            IM_QQ = imqq;
        }
    }
}
