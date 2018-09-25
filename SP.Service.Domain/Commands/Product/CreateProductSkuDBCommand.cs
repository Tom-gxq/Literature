using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class CreateProductSkuDBCommand :  Command
    {
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public int Stock { get; set; }

        public CreateProductSkuDBCommand(Guid id, string accountId, string productId, int shopId, int stock)
        {
            base.Id = id;
            this.AccountId = accountId;
            this.ShopId = shopId;
            this.Stock = stock;
            this.ProductId = productId;
        }
    }
}
