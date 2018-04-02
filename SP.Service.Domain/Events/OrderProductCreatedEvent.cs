using Grpc.Service.Core.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class OrderProductCreatedEvent : Event
    {
        public OrdersEntity Order { get; internal set; }
        public int VersionId { get; internal set; }
        public OrderProductCreatedEvent(Guid aggregateId, OrdersEntity order, int versionId)
        {
            AggregateId = aggregateId;
            Order = order;
            VersionId = versionId;
        }
    }
}
