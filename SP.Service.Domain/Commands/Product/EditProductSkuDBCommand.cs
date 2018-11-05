using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class EditProductSkuDBCommand : Command
    {
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public int Stock { get; set; }
        public int Type { get; set; }

        public EditProductSkuDBCommand(Guid id, string accountId, string productId, int shopId, int stock,int type) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.AccountId = accountId;
            this.ShopId = shopId;
            this.Stock = stock;
            this.ProductId = productId;
            this.Type = type;
            this.CommandType = CommandType.EditProductSkuDB;
        }
    }
}
