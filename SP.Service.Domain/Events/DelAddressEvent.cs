using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class DelAddressEvent: Event
    {
        public int AddressId { get; set; }
        public DelAddressEvent(int addressId)
        {
            this.AddressId = addressId;
        }
    }
}
