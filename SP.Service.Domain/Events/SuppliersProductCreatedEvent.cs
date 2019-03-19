using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public  class SuppliersProductCreatedEvent : Event
    {
        public int SuppliersId { get; set; }
        public string ProductId { get; set; }
        public double PurchasePrice { get; set; }
        public int Status { get; set; }
        public int AlertStock { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public SuppliersProductCreatedEvent(Guid id, string productId, int suppliersId,double purchasePrice, int alertStock)
        {
            this.CommandId = id.ToString();
            this.EventType = EventType.SuppliersProductCreated;
            this.ProductId = productId;
            this.SuppliersId = suppliersId;
            this.PurchasePrice = purchasePrice;
            this.AlertStock = alertStock;
            this.CreateTime = DateTime.Now;
            this.Status = 0;
        }
    }
}
