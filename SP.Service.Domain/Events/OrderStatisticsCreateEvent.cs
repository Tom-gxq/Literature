using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class OrderStatisticsCreateEvent : OrderStatisticsSumEvent
    {
        public OrderStatisticsCreateEvent(string orderId, string orderCode, string accountId, double foodAmount,double markAmount, int addressId, DateTime orderDate) :base(orderId, orderCode, accountId, foodAmount, markAmount, addressId, orderDate)
        {

        }
    }
}
