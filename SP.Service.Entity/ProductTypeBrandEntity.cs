using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ProductTypeBrand")]
    public class ProductTypeBrandEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public int? TypeId { get; set; }
        public string BrandId { get; set; }
    }
}
