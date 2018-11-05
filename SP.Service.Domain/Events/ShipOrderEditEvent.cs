using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ShipOrderEditEvent: OrderEditEvent
    {
        public string AccountId { get; set; }
        public ShipOrderEditEvent(Guid aggregateId, OrderStatus orderStatus, OrderPay payWay,string accountId)
            :base(aggregateId, orderStatus, payWay)
        {
            this.AccountId = accountId;
            this.EventType = Grpc.Service.Core.Domain.Events.EventType.ShipOrderEdit;
        }
    }
}
