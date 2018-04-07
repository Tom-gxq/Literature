using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class RedoProductSkuEvent : Event
    {
        public int ShopId { get; set; }
        public string ProductId { get; set; }
        public int RedoStock { get; set; }
        public RedoProductSkuEvent(int shopId, string productId, int redoStock)
        {
            this.ProductId = productId;
            this.RedoStock = redoStock;
            this.ShopId = shopId;
        }
    }
}
