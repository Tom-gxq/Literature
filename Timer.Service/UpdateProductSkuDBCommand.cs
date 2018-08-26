using SP.Service.Domain.Commands.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timer.Service
{
    public class UpdateProductSkuDBCommand: EditProductSkuDBCommand
    {
        public string EventType { get; set; }
        public UpdateProductSkuDBCommand(Guid id, string accountId, string productId, int shopId, int stock,int type) :base(id,accountId,  productId, shopId, stock, type)
        {

        }
    }
}
