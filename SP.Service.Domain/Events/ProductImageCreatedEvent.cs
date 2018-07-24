using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductImageCreatedEvent : Event
    {
        public string ImagePath { get; set; }
        public ProductImageCreatedEvent(Guid id, string imagePath)
        {
            base.AggregateId = id;
            this.ImagePath = imagePath;
        }
    }
}
