using SP.Api.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Order
{
    public class OrderInfoModel
    {
        public string orderId { get; set; }
        public string remark { get; set; }
        public int orderStatus { get; set; }
        public double amount { get; set; }
        public string orderCode { get; set; }
        public long orderDate { get; set; }

        public List<ProductModel> productList { get; set; }
        public CouponsModel Coupons { get; set; }
        public string shopType { get; set; }

    }
}
