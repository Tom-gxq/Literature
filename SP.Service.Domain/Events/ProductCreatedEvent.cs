using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductCreatedEvent : ProductEditEvent
    {
        public long MainType { get; set; }
        public long SecondType { get; set; }        
        public string AccountId { get; set; }

        public ProductCreatedEvent(Guid id, long mainType, long secondType, string productId,
            string accountId, double purchasePrice, int suppliersId) :base(id,productId, purchasePrice, suppliersId)
        {
            this.CommandId = id.ToString();
            this.MainType = mainType;
            this.SecondType = secondType;
            this.AccountId = accountId;
            this.EventType = EventType.ProductCreated;
        }
    }
}
