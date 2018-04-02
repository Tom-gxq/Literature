using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ProductSKUs")]
    public class ProductSkuEntity : BaseEntity
    {
        [Ignore]
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string SkuId { get; set; }
        public string SKU { get; set; }
        public DateTime? EffectiveTime { get; set; }
        public int? Stock { get; set; }
        public int? AlertStock { get; set; }
        public double? Price { get; set; }
    }
}
