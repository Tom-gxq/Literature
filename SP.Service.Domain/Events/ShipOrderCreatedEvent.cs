using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ShipOrderCreatedEvent: Event
    {
        public string OrderId { get; internal set; }
        public string ShippingId { get; internal set; }
        public string ShipTo { get; internal set; }
        public DateTime ShippingDate { get; internal set; }
        public int Stock { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public ShipOrderCreatedEvent(string orderId, string shippingId, string shipTo, DateTime shippingDate,
            int stock, string productId, int shopId)
        {
            this.OrderId = orderId;
            this.ShippingId = shippingId;
            this.ShipTo = shipTo;
            this.ShippingDate = shippingDate;
            this.Stock = stock;
            this.ProductId = productId;
            this.ShopId = shopId;
        }
    }
}
