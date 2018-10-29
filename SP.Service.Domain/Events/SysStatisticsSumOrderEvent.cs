using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsSumOrderEvent : OrderStatisticsSumEvent
    {
        public SysStatisticsSumOrderEvent(string orderId, string orderCode, string accountId, double foodAmount, double markAmount, int addressId, DateTime orderDate):base(orderId, orderCode, accountId, foodAmount, markAmount, addressId, orderDate)
        {
           
        }
    }
}
