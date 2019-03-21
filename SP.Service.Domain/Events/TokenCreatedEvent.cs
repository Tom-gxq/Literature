using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public  class TokenCreatedEvent:Event
    {
        public string AccessToken { get;  set; }
        public string AccountId { get;  set; }
        public bool Status { get;  set; }
        public DateTime CreateTime { get;  set; }
        public TokenCreatedEvent(Guid id,string token,string accountId,bool status,DateTime createTime)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.AccessToken = token;
            this.AccountId = accountId;
            this.Status = status;
            this.CreateTime = createTime;
            this.EventType = EventType.TokenCreated;
        }
    }
}
