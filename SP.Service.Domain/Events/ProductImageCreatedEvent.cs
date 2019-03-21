using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductImageCreatedEvent : Event
    {
        public string ImagePath { get; set; }
        public ProductImageCreatedEvent(Guid id, string imagePath) : base(KafkaConfig.EventBusTopicTitle)
        {
            base.AggregateId = id;
            this.CommandId = id.ToString();
            this.ImagePath = imagePath;
            this.EventType = EventType.ProductImageCreated;
        }
    }
}
