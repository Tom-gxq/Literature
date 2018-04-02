using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ProductAttribute")]
    public class ProductAttributeEntity : BaseEntity
    {
        [AutoIncrement]
        [Alias("AutoID")]
        public int? ID { get; set; }
        public string ProductId { get; set; }
        public long? AttributeId { get; set; }
        public long? ValueId { get; set; }
    }
}
