using SP.Api.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class ShoppingCartModel
    {
        public string CartId { get;  set; }
        public string AccountId { get;  set; }
        public string ProductId { get;  set; }
        public int Quantity { get;  set; }
        public string OrderId { get;  set; }
        public DateTime CreateTime { get;  set; }
        public DateTime UpdateTime { get;  set; }
        public ProductModel Product { get;  set; }
        public int ShopId { get; set; }
        public double Amount { get; set; }
        public double VIPAmount { get; set; }
        public int ShipOrderId { get; set; }
    }
}
