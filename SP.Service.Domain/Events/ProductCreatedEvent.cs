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
        public string SuppliersId { get; set; }
        public double VipPrice { get; set; }

        public ProductCreatedEvent(Guid id, long mainType, long secondType, string productName,
            string suppliersId, double marketPrice, double purchasePrice, double vipPrice) :base(id,productName, marketPrice, purchasePrice)
        {
            this.MainType = mainType;
            this.SecondType = secondType;
            this.SuppliersId = suppliersId;
            this.VipPrice = vipPrice;
            this.EventType = EventType.ProductCreated;
        }
    }
}
