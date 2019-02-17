using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ProductRegion")]
    public class ProductRegionEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string ProductId { get; set; }
        public int? RegionID { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
