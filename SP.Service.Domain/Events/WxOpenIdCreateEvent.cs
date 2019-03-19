using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class WxOpenIdCreateEvent : Event
    {
        public string AccountId { get; set; }
        public string WxOpenId { get; set; }
        public int WxType { get; set; }
        public DateTime CreateTime { get; set; }

        public WxOpenIdCreateEvent(Guid id, string accountId, string wxOpenId,int wxType)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.AccountId = accountId;
            this.WxOpenId = wxOpenId;
            this.WxType = wxType;
            this.CreateTime = DateTime.Now;
            this.EventType = EventType.WxOpenIdCreate;
        }
    }
}
