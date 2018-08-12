using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class ProductModel
    {
        /**
        * 商品分类
        */
        public long MainType { get; set; }
        /**
        * 商品小分类
        */
        public long SecondType { get; set; }
        /**
        * 商品名
        */
        public string ProductName { get; set; }
        /**
        * 商品图片路径
        */
        public string imagePath { get; set; }
        /**
        * 售货价格
        */
        public double MarketPrice { get; set; }
        /**
        * 拿货价格
        */
        public double PurchasePrice { get; set; }
        public string AccountId { get; set; }
        public bool SaleStatus { get; set; }
    }
}