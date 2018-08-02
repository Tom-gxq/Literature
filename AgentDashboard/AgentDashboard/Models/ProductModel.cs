using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class ProductModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ShortDescription { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public int SaleStatus { get; set; }
        public int ShopId { get; set; }
        public DateTime AddedDate { get; set; }
        public int DisplaySequence { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal VIPPrice { get; set; }
        public DateTime UpdateTime { get; set; }
        public string LastOperater { get; set; }
        public int TypeId { get; set; }
        public int SecondTypeId { get; set; }
        public string ProductImage { get; set; }
        public int Quantity { get; set; }
    }
}