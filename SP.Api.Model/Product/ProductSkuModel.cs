using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Product
{
    public class ProductSkuModel
    {
        public string productId { get; set; }
        public string skuId { get; set; }
        public int stock{ get; set; }
        public int shopId { get; set; }
        public string accountId { get; set; }
    }
}
