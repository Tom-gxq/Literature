using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ShipOrderEditEvent: OrderEditEvent
    {
        public ShipOrderEditEvent(Guid aggregateId, OrderStatus orderStatus, OrderPay payWay)
            :base(aggregateId, orderStatus, payWay)
        {
            
        }
    }
}
