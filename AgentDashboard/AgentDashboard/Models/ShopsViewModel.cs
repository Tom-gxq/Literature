using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class ShopsViewModel
    {
        public List<ShopViewModel> ShopList { get; set; }
        public List<RegionViewModel> UniversityList { get; set; }
        public List<RegionViewModel> ColleageList { get; set; }
        public int UniversityId { get; set; }
    }
}