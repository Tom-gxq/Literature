using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class DelAddressEvent: Event
    {
        public int AddressId { get; set; }
        public DelAddressEvent(Guid id, int addressId) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.AddressId = addressId;
            this.EventType = EventType.DelAddress;
        }
    }
}
