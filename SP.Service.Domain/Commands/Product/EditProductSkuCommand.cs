﻿using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class EditProductSkuCommand : Command
    {
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public int Stock { get; set; }
        public int Type { get; set; }

        public EditProductSkuCommand(Guid id, string accountId, string productId,int shopId,int stock,int type) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.AccountId = accountId;
            this.ShopId = shopId;
            this.Stock = stock;
            this.ProductId = productId;
            this.Type = Type;
            this.CommandType = CommandType.EditProductSku;
        }
    }
}
