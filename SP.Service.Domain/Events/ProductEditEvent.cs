using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductEditEvent : Event
    {
        public string ProductName { get; set; }
        public double MarketPrice { get; set; }
        public double PurchasePrice { get; set; }
        public ProductEditEvent(Guid id,  string productName,double marketPrice, double purchasePrice)
        {
            base.AggregateId = id;
            this.ProductName = productName;
            this.MarketPrice = marketPrice;
            this.PurchasePrice = purchasePrice;
        }
    }
}
