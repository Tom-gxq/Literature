using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class ShopManagerViewModel
    {
        public List<SellerViewModel> Sellers;
        public Dictionary<int, String> Universities { get; set; }
        public int UniversityIdx { get; set; }

        public Dictionary<int, String> TypeList { get; set; }
        public int TypeId { get; set; }
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
    }
}