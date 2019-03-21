using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class OrderProductCreatedEvent : Event
    {
        public OrdersEntity Order { get; set; }
        public int VersionId { get; set; }
        public OrderProductCreatedEvent(Guid aggregateId, OrdersEntity order, int versionId)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            CommandId = aggregateId.ToString();
            Order = order;
            VersionId = versionId;
            EventType = EventType.OrderProductCreated;
        }
    }
}
