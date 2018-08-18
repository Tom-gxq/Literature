using SP.Api.Model.Account;
using SP.Api.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Order
{
    public class LeadOrderModel
    {
        public string orderId { get; set; }
        public string remark { get; set; }
        public int orderStatus { get; set; }
        public double amount { get; set; }
        public string orderCode { get; set; }
        public string orderDate { get; set; }
        public long payDate { get; set; }
        public AccountModel account { get; set; }
        public AddressModel address { get; set; }
        public ShopModel shop { get; set; }        
        public List<ShoppingCartModel> shoppingCartList { get; set; }
    }
}
