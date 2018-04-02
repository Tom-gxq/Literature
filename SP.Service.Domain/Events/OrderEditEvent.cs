﻿using Grpc.Service.Core.Domain.Events;
using SP.Data.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class OrderEditEvent : Event
    {
        public OrderStatus OrderStatus { get; internal set; }
        public OrderEditEvent(Guid aggregateId,OrderStatus orderStatus)
        {
            AggregateId = aggregateId;
            OrderStatus = orderStatus;
        }
    }
}
