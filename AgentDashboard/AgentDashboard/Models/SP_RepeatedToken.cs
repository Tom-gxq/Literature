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
    
    public partial class SP_RepeatedToken
    {
        public int Id { get; set; }
        public string AccessToken { get; set; }
        public string AccountID { get; set; }
        public bool Status { get; set; }
        public System.DateTime CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
    }
}
