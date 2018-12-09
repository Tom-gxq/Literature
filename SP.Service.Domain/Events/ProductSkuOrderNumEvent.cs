﻿using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class ProductSkuOrderNumEvent : Event
    {
        public int ShopId { get; set; }
        public string ProductId { get; set; }
        public string AccountId { get; set; }
        public int OrderNum { get; set; }
        public ProductSkuOrderNumEvent(Guid id,int shopId, string productId, string accountId, int orderNum)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.ProductId = productId;
            this.OrderNum = orderNum;
            this.ShopId = shopId;
            this.AccountId = accountId;
            this.EventType = EventType.ProductSkuOrderNum;
        }
    }
}
