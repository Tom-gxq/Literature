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
        public string OrderId { get; set; }
        public string Host { get; set; }
        public string AccountId { get; set; }
        public RedoProductSkuEvent(int shopId, string productId, int redoStock, string orderId, string host, string accountId)
        {
            this.ProductId = productId;
            this.RedoStock = redoStock;
            this.ShopId = shopId; this.OrderId = orderId;
            this.Host = host;
            this.AccountId = accountId;
        }
    }
}
