using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class DataAnalyzeViewModel
    {
        public int RegionId { get; set; }

        public Dictionary<int, String> Universities { get; set; }
    }
}