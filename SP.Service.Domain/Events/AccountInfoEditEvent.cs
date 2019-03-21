using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccountInfoEditEvent : Event
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
        public int UserType { get; set; }
        public AccountInfoEditEvent(Guid id,string accountId, string userName, int gender, string avatar,int userType)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.Avatar = avatar;
            this.FullName = userName;
            this.Gender = gender;
            this.AccountId = accountId;
            this.UserType = userType;
            this.EventType = EventType.AccountInfoEdit;
        }
    }
}
