using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ResidueSkuUpdateEvent : Event
    {
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public int Stock { get; set; }

        public ResidueSkuUpdateEvent(Guid id, int stock) : base(KafkaConfig.EventBusTopicTitle)
        {
            base.AggregateId = id;
            this.Stock = stock;
            this.EventType = EventType.ResidueSkuUpdate;
        }
    }
}
