//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgentDashboard.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SP_ShippingOrders
    {
        public int ShippingOrderId { get; set; }
        public string OrderId { get; set; }
        public string ManagerRemark { get; set; }
        public Nullable<System.DateTime> ShippingDate { get; set; }
        public Nullable<System.DateTime> ShippedDate { get; set; }
        public string ShippingId { get; set; }
        public string ShipTo { get; set; }
        public string ShipOrderNumber { get; set; }
        public Nullable<bool> IsShipped { get; set; }
        public Nullable<int> Stock { get; set; }
        public string ProductId { get; set; }
        public Nullable<int> ShopId { get; set; }
    }
}
