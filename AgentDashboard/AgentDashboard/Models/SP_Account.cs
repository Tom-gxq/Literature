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
    
    public partial class SP_Account
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte Status { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.DateTime> UpdateTime { get; set; }
        public string AliBind { get; set; }
        public string WxBind { get; set; }
        public string QQBind { get; set; }
    }
}
