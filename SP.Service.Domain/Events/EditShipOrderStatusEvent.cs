using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class EditShipOrderStatusEvent : Event
    {
        public int ShipOrderId { get; set; }
        public bool IsShiped { get; set; }
        public EditShipOrderStatusEvent(Guid aggregateId,int shipOrderId,bool isShiped)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = aggregateId.ToString();
            this.ShipOrderId = shipOrderId;
            this.IsShiped = isShiped;
            this.EventType = EventType.EditShipOrderStatus;
        }
    }
}
