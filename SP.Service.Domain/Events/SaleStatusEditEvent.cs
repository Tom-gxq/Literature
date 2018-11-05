using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SaleStatusEditEvent: Event
    {
        public int Status { get; set; }
        public SaleStatusEditEvent(Guid id, int status) : base(KafkaConfig.EventBusTopicTitle)
        {
            base.AggregateId = id;
            this.Status = status;
            this.EventType = EventType.SaleStatusEdit;
        }
    }
}
