using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;

namespace SP.Service.Domain.Events
{
    public class AddShoppingCartNumEvent : Event
    {
        public string CartId { get;  set; }
        public string AccountId { get;  set; }
        public int Quantity { get;  set; }
        public DateTime CreateTime { get;  set; }
        public string ProductId { get;  set; }
        public int ShopId { get; set; }
        public AddShoppingCartNumEvent(Guid id,string accountId, string cartId, int quantity, string productId, DateTime createTime, int shopId)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            CommandId = id.ToString();
            CartId = cartId;
            AccountId = accountId;
            Quantity = quantity;
            ProductId = productId;
            CreateTime = createTime;
            ShopId = shopId;
            EventType = EventType.AddShoppingCartNum;
        }
    }
}
