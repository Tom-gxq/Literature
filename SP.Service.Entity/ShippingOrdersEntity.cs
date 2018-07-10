using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ShippingOrders")]
    public class ShippingOrdersEntity : BaseEntity
    {
        [AutoIncrement]
        [Alias("ShippingOrderId")]
        public int? Id { get; set; }
        public string OrderId { get; set; }
        public string ShippingId { get; set; }
        public string ShipTo { get; set; }
        public bool? IsShipped { get; set; }
        public DateTime? ShippingDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? Stock { get; set; }
        public string ProductId { get; set; }
        public int? ShopId { get; set; }
    }
}
