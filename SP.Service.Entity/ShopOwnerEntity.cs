using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ShopOwner")]
    public class ShopOwnerEntity : BaseEntity
    {
        [AutoIncrement]
        public int? ShopId { get; set; }
        public string OwnerId { get; set; }
        public bool? ShopStatus { get; set; }
    }
}
