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
    
    public partial class SP_SysStatistics
    {
        public System.DateTime CreateTime { get; set; }
        public Nullable<int> Num_NewUser { get; set; }
        public Nullable<int> Num_NewOrder { get; set; }
        public Nullable<int> Num_OrderAmount { get; set; }
        public Nullable<int> Num_NewAssociator { get; set; }
        public Nullable<int> Num_BuyAssociator { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public bool IsChecked { get; set; }
    }
}
