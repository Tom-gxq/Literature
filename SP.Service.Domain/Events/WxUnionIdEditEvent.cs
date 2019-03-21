using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class WxUnionIdEditEvent : Event
    {
        public string AccountId { get; set; }
        public string WxUnionId { get; set; }
        public DateTime CreateTime { get; set; }

        public WxUnionIdEditEvent(Guid id,string accountId, string wxUnionId)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.AccountId = accountId;
            this.WxUnionId = wxUnionId;
            this.CreateTime = DateTime.Now;
            this.EventType = EventType.WxUnionIdEdit;
        }
    }
}
