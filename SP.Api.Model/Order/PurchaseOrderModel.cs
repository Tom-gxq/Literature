using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Order
{
    public class PurchaseOrderModel: PurchaseOrderBaseModel
    {        
        public string orderCode { get; set; }
        public string payDate { get; set; }
        public int payType { get; set; }
        public int orderStatus { get; set; }
        public AccountInfo account { get; set; }
        public string address { get; set; }
        public List<ShoppingCartModel> shoppingCartList { get; set; }
    }
}
