using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class UpdateShoppingCartOrderIDEvent : Event
    {
        public string CartId { get; set; }
        public string OrderId { get; set; }
        public UpdateShoppingCartOrderIDEvent(string cartId, string orderId) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CartId = cartId;
            this.OrderId = orderId;
            this.EventType = EventType.UpdateShoppingCartOrderID;
        }
    }
}
