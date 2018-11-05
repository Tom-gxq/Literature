using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class DecreaseProductSkuEvent : Event
    {
        public int ShopId { get; set; }
        public string ProductId { get; set; }
        public int DecStock { get; set; }
        public string OrderId { get; set; }
        public string Host { get; set; }
        public string AccountId  { get; set; }
        public DecreaseProductSkuEvent(int shopId, string productId,int decStock, string orderId, string host,string accountId)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.ProductId = productId;
            this.DecStock = decStock;
            this.ShopId = shopId;
            this.OrderId = orderId;
            this.Host = host;
            this.AccountId = accountId;
            this.EventType = EventType.DecreaseProductSku;
        }
    }
}
