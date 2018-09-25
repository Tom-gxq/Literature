using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductSkuEditEvent : Event
    {
        public string Host { get; set; }
        public string AccountId { get; set; }
        public ProductSkuEditEvent(Guid aggregateId,string host, string accountId)
        {
            this.AggregateId = aggregateId;
            this.Host = host;
            this.AccountId = accountId;
        }
    }
}
