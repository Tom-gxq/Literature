using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Product
{
    public class SellerProductModel
    {
        /**
        * 商品分类
        */
        public long mainType { get; set; }
        /**
        * 商品小分类
        */
        public long secondType { get; set; }
        /**
        * 商品名
        */
        public string productName { get; set; }
        /**
        * 商品图片路径
        */
        public string imagePath { get; set; }
        /**
        * 售货价格
        */
        public double marketPrice { get; set; }
        /**
        * 拿货价格
        */
        public double purchasePrice { get; set; }
        public string accountId { get; set; }
        public string productId { get; set; }
        public bool saleStatus { get; set; } 
    }
}
