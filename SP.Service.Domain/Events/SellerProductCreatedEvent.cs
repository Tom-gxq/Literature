using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SellerProductCreatedEvent : Event
    {
        public string AccountId { get; internal set; }
        public int SupplierProductId { get; internal set; }

        public SellerProductCreatedEvent(Guid id, string accountId, int suppliersId) 
        {
            this.CommandId = id.ToString();          
            this.SupplierProductId = suppliersId;
            this.AccountId = accountId;
            this.EventType = EventType.SellerProductCreated;
        }
    }
}
