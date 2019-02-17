using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_SuppliersRegion")]
    public class SuppliersRegionEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public int? SuppliersId { get; set; }
        public int? RegionID { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
