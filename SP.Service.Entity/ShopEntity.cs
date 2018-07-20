using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_Shop")]
    public class ShopEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public string ShopName { get; set; }
        public int? DisplaySequence { get; set; }
        public string OwnerId { get; set; }
        public string MetaKeywords { get; set; }
        public int? RegionId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public int? ShopType { get; set; }
        public string ShopLogo { get; set; }
        public bool? ShopStatus { get; set; }
    }
}
