using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class ShopViewModel
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public string OwnerId { get; set; }
        public Nullable<int> DisplaySequence { get; set; }
        public string MetaKeywords { get; set; }
        public Nullable<int> RegionId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Nullable<int> ShopType { get; set; }
        public string ShopLogo { get; set; }
        public Nullable<bool> ShopStatus { get; set; }
        public string BuildingName { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
    }
}