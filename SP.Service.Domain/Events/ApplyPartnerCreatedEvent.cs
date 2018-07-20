using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ApplyPartnerCreatedEvent:Event
    {
        public int DormId { get; set; }
        public ApplyPartnerCreatedEvent(Guid id,int dormId)
        {
            this.AggregateId = id;
            this.DormId = dormId;
        }
    }
}
