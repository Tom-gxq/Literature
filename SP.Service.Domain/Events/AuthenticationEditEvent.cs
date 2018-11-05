using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AuthenticationEditEvent : Event
    {
        public int AuthType { get; set; }
        public string AccountId { get; set; }
        public string Account { get; set; }
        public int Status { get; set; }

        public AuthenticationEditEvent(Guid aggregateId, int authType, string accountId, string account, int status)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            base.AggregateId = aggregateId;
            this.AuthType = authType;
            this.AccountId = accountId;
            this.Account = account;
            this.Status = status;
            this.EventType = EventType.AuthenticationEdit;
        }
    }
}
