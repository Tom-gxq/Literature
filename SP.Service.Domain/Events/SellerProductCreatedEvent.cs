using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SellerProductCreatedEvent : Event
    {
        public string AccountId { get;  set; }
        public int SupplierProductId { get;  set; }

        public SellerProductCreatedEvent(Guid id, string accountId, int suppliersId) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();          
            this.SupplierProductId = suppliersId;
            this.AccountId = accountId;
            this.EventType = EventType.SellerProductCreated;
        }
    }
}
