using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class DecreaseProductSkuEvent : Event
    {
        public string ProductId { get; set; }
        public int DecStock { get; set; }
        public DecreaseProductSkuEvent(string productId,int decStock)
        {
            this.ProductId = productId;
            this.DecStock = decStock;
        }
    }
}
