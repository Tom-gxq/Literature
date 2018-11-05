using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class BaseBindEvent : Event
    {
        public string OtherAccount { get; set; }
        public BaseBindEvent(Guid id, string otherAccount) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.AggregateId = id;
            this.OtherAccount = otherAccount;
        }
    }
    public class AliBindEvent : BaseBindEvent
    {
        public AliBindEvent(Guid id, string otherAccount) : base(id, otherAccount)
        {
            this.EventType = EventType.AliBind;
        }
    }
    public class WxBindEvent : BaseBindEvent
    {
        public WxBindEvent(Guid id, string otherAccount) : base(id, otherAccount)
        {
            this.EventType = EventType.WxBind;
        }
    }
    public class QQBindEvent : BaseBindEvent
    {
        public QQBindEvent(Guid id, string otherAccount) : base(id, otherAccount)
        {
            this.EventType = EventType.QQBind;
        }
    }
}
