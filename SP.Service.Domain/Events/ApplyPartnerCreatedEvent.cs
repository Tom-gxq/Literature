using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ApplyPartnerCreatedEvent:Event
    {
        public int DormId { get; set; }
        public ApplyPartnerCreatedEvent(Guid id,int dormId) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.AggregateId = id;
            this.DormId = dormId;
            this.EventType = EventType.ApplyPartnerCreated;
        }
    }
}
