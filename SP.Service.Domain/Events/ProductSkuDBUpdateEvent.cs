using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductSkuDBUpdateEvent : Event
    {
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public int Stock { get; set; }
        public int Type { get; set; }


        public ProductSkuDBUpdateEvent(Guid id, string accountId, string productId, int shopId, int stock,int type)
        {
            base.AggregateId = id;
            this.AccountId = accountId;
            this.ShopId = shopId;
            this.Stock = stock;
            this.ProductId = productId;
            this.Type = type;
        }
    }
}
