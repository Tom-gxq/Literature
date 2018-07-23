﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class ShopDetailsViewModel
    {
        public String ShopName { get; set; }
        public int DeliverManCount { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<DeliverManViewModel> DeliverMen { get; set; }
    }

    public class DeliverManViewModel
    {
        public string Name { get; set; }

        public List<ProductsViewModel> Products { get; set; }
    }

    public class ProductsViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int PreStocks { get; set; }
        public int Stocks { get; set; }
    }
}
