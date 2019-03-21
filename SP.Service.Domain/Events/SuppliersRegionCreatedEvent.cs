using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SuppliersRegionCreatedEvent : Event
    {
        public int SuppliersId { get; set; }
        public int RegionID { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public SuppliersRegionCreatedEvent(Guid id,  int suppliersId, int regionId)
        {
            this.CommandId = id.ToString();
            this.EventType = EventType.SuppliersRegionCreated;
            this.SuppliersId = suppliersId;
            this.CreateTime = DateTime.Now;
            this.RegionID = regionId;
        }
    }
}
