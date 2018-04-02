using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class CreatShoppingCartEvent : Event
    {
        public string CartId { get; internal set; }
        public string AccountId { get; internal set; }
        public int Quantity { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public string ProductId { get; internal set; }
        public int ShopId { get; set; }

        public CreatShoppingCartEvent(string accountId, string cartId, int quantity,string productId, DateTime createTime, int shopId)
        {
            CartId = cartId;
            AccountId = accountId;
            Quantity = quantity;
            ProductId = productId;
            CreateTime = createTime;
            ShopId = shopId;
        }
    }
}
