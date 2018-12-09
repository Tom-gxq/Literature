using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class TokenDisabledEvent : Event
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public bool Status { get; set; }
        public DateTime UpdateTime { get; set; }
        public TokenDisabledEvent(Guid id, string token, string accountId, bool status, DateTime updateTime)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.AccessToken = token;
            this.AccountId = accountId;
            this.Status = status;
            this.UpdateTime = updateTime;
            this.EventType = EventType.TokenDisabled;
        }
    }
}
