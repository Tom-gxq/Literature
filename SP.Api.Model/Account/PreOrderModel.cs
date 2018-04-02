using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class PreOrderModel
    {
        public List<ShoppingCartModel> shoppingCartList { get; set; }
        public AddressModel defaultAddress { get; set; }
        public double amoutTotal { get; set; }
    }
}
