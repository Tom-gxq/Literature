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
        public int SelectIndex { get; set; }
    }
}