using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AccessTokenDelEvent : Event
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }

        public AccessTokenDelEvent(Guid id,string accountId, string accessToken)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            CommandId = id.ToString();
            AccessToken = accessToken;
            AccountId = accountId;
            EventType = EventType.AccessTokenDel;
        }
    }
}
