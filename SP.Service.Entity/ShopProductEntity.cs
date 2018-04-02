using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ShopProduct")]
    public class ShopProductEntity : BaseEntity
    {
        [AutoIncrement]
        public int? ShopId { get; set; }
        public string ProductId { get; set; }
    }
}
