using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsSumOrderEvent : OrderStatisticsSumEvent
    {
        public SysStatisticsSumOrderEvent(Guid id,string orderId, string orderCode, string accountId, double foodAmount, double markAmount, int addressId, DateTime orderDate):base(id,orderId, orderCode, accountId, foodAmount, markAmount, addressId, orderDate)
        {
            this.EventType = Grpc.Service.Core.Domain.Events.EventType.SysStatisticsSumOrder;
        }
    }
}
