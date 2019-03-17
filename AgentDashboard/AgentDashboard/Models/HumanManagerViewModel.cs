using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class HumanManagerViewModel
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string CellPhoneNo { get; set; }
        public string RegionName { get; set; }
        public string TypeName { get; set; }
        public String Birthday { get; set; }
        public int ProductType { get; set; }
    }
}