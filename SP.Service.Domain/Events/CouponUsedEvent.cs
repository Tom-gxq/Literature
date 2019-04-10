using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class CouponUsedEvent : Event
    {
        public int Status { get; set; }
        public CouponUsedEvent(Guid aggregateId, int status)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            CommandId = aggregateId.ToString();
            Status = status;
            EventType = EventType.CouponUsed;
        }
    }
}
