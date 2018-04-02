using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    public class ProductFullEntity: ProductEntity
    {
        public int Quantity { get; set; }
        public int ShopId { get; set; }
    }
}
