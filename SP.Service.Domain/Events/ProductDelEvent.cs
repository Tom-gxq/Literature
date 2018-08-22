using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductDelEvent : Event
    {
        public ProductDelEvent(Guid id)
        {
            this.AggregateId = id;
        }
    }
}
