using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    public class ShippingOrderFullEntity: ShippingOrdersEntity
    {
        public string OrderAddress { get; set; }
        public bool? IsVip { get; set; }
        public bool? IsWxPay { get; set; }
        public bool? IsAliPay { get; set; }
        public int? OrderType { get; set; }
        public string ProductName { get; set; }
        public double? MarketPrice { get; set; }
        public double? VIPPrice { get; set; }
        public double? PurchasePrice { get; set; }
        public long? SecondTypeId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? PayDate { get; set; }
        public int? ShopId { get; set; }
    }
}
