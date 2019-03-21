using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class DelShoppingCartEvent : Event
    {
        public string CartId { get; set; }
        public DelShoppingCartEvent(Guid aggregateId,string cartId) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = aggregateId.ToString();
            this.CartId = cartId;
            this.EventType = EventType.DelShoppingCart;
        }
    }
}
