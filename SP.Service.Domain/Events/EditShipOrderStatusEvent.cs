using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class EditShipOrderStatusEvent : Event
    {
        public int ShipOrderId { get; set; }
        public bool IsShiped { get; set; }
        public EditShipOrderStatusEvent(int shipOrderId,bool isShiped)
        {
            this.ShipOrderId = shipOrderId;
            this.IsShiped = isShiped;
        }
    }
}
