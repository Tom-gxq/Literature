using Grpc.Service.Core.Domain.Events;
using SP.Data.Enum;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class OrderEditEvent : Event
    {
        public OrderStatus OrderStatus { get; set; }
        public OrderPay PayWay { get; set; }
        public OrderEditEvent(Guid aggregateId,OrderStatus orderStatus, OrderPay payWay)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            OrderStatus = orderStatus;
            PayWay = payWay;
            EventType = EventType.OrderEdit;
        }
    }
}
