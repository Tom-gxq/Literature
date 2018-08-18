using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductSkuEditEvent : Event
    {
        public string Host { get; set; }
        public ProductSkuEditEvent(Guid aggregateId,string host)
        {
            this.AggregateId = aggregateId;
            this.Host = host;
        }
    }
}
