using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AssociatorEditEvent : Event
    {
        public int Status { get; set; }
        public AssociatorEditEvent(Guid aggregateId, int status) : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            Status = status;
            EventType = EventType.AssociatorEdit;
        }
    }
}
