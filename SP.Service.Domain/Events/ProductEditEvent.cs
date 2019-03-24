using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductEditEvent : Event
    {
        public string ProductId { get; set; }
        public int SuppliersId { get; set; }
        public double PurchasePrice { get; set; }
        public ProductEditEvent(Guid id,  string productId, double purchasePrice, int suppliersId)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            base.AggregateId = id;
            this.CommandId = id.ToString();
            this.ProductId = productId;
            this.PurchasePrice = purchasePrice;
            this.SuppliersId = suppliersId;
            this.EventType = EventType.ProductEdit;
        }
    }
}
