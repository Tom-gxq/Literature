using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductSkuDBCreateEvent : Event
    {
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public int Stock { get; set; }

        public ProductSkuDBCreateEvent(Guid id, string accountId, string productId, int shopId, int stock)
        {
            base.AggregateId = id;
            this.AccountId = accountId;
            this.ShopId = shopId;
            this.Stock = stock;
            this.ProductId = productId;
        }
    }
}
