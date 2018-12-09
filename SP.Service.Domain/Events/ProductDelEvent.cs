using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductDelEvent : Event
    {
        public ProductDelEvent(Guid id) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.AggregateId = id;
            this.CommandId = id.ToString();
            this.EventType = EventType.ProductDel;
        }
    }
}
