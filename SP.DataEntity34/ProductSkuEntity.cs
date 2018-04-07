using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_ProductSKUs")]
    public class ProductSkuEntity : Entity<string>
    {
        [Alias("SkuId")]
        public override string Id { get; set; }
        public int? ShopId { get; set; }
        public string AccountId { get; set; }
        public  string ProductId { get; set; }
        public  string SKU { get; set; }
        public DateTime? EffectiveTime { get; set; }
        public int? Stock { get; set; }
        public int? AlertStock { get; set; }
        public double? Price { get; set; }        
        public int? OrderNum { get; set; }
    }
}
