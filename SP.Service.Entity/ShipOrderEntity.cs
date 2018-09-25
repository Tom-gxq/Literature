using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ShipOrder")]
    public class ShipOrderEntity : BaseEntity
    {
        [AutoIncrement]
        [Alias("ShipOrderId")]
        public int? Id { get; set; }
        public string OrderId { get; set; }
        public string ShipId { get; set; }
        public string ShipTo { get; set; }
        public bool? IsShipped { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? Stock { get; set; }
        public string ProductId { get; set; }
        public int? ShopId { get; set; }
    }
}
