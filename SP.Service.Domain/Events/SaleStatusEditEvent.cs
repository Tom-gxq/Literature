using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SaleStatusEditEvent: Event
    {
        public int Status { get; set; }
        public SaleStatusEditEvent(Guid id, int status)
        {
            base.AggregateId = id;
            this.Status = status;
        }
    }
}
