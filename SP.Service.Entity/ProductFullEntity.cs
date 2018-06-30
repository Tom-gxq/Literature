using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    public class ProductFullEntity: ProductEntity
    {
        public int? Stock { get; set; }
        public int? AlertStock { get; set; }
        public double? Price { get; set; }
        public string SkuId { get; set; }
        public int? ShopId { get; set; }
    }
}
