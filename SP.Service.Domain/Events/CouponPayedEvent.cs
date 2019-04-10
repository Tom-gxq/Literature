using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class CouponPayedEvent : Event
    {
        public int PayType { get; set; }
        public double PayAmount { get; set; }

        public CouponPayedEvent(Guid aggregateId, double payAmount, int payType)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            CommandId = aggregateId.ToString();
            PayType = payType;
            PayAmount = payAmount;
            EventType = EventType.CouponPayed;
        }
    }
}
