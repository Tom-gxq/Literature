using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class CreatShoppingCartEvent : Event
    {
        public string CartId { get;  set; }
        public string AccountId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateTime { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public string ShiperId { get; set; }

        public CreatShoppingCartEvent(string accountId, string cartId, int quantity,string productId, DateTime createTime, int shopId, string shiperId)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            CartId = cartId;
            AccountId = accountId;
            Quantity = quantity;
            ProductId = productId;
            CreateTime = createTime;
            ShopId = shopId;
            ShiperId = shiperId;
            this.EventType = EventType.CreatShoppingCart;
        }
    }
}
