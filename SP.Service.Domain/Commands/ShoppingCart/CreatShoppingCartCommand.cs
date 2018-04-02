using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.ShoppingCart
{
    public class CreatShoppingCartCommand : Command
    {
        public string ProductId { get; set; }
        public string AccountId { get; set; }
        /**
        *  购买数量
        */
        public int Quantity { get; set; }
        /**
        *  店铺ID
*/
        public int ShopId { get; set; }
        public CreatShoppingCartCommand(Guid id, string productId, string accountId, int quantity, int shopId)
        {
            base.Id = id;
            this.ShopId = shopId;
            this.ProductId = productId;
            this.AccountId = accountId;
            this.Quantity = quantity;
        }
    }
}
