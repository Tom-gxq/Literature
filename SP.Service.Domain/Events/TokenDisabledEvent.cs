using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class TokenDisabledEvent : Event
    {
        public string AccessToken { get; internal set; }
        public string AccountId { get; internal set; }
        public bool Status { get; internal set; }
        public DateTime UpdateTime { get; internal set; }
        public TokenDisabledEvent(string token, string accountId, bool status, DateTime updateTime)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.AccessToken = token;
            this.AccountId = accountId;
            this.Status = status;
            this.UpdateTime = updateTime;
            this.EventType = EventType.TokenDisabled;
        }
    }
}
