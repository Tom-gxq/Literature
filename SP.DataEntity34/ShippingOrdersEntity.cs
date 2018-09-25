using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_ShippingOrders")]
    public class ShippingOrdersEntity : Entity
    {
        [AutoIncrement]
        [Alias("ShippingOrderId")]
        public override int Id
        {
            get
            {
                return base.Id;
            }

            set
            {
                base.Id = value;
            }
        }
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
