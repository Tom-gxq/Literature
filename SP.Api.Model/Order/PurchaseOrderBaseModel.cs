using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Order
{
    public class PurchaseOrderBaseModel
    {
        public string orderId { get; set; }
        public string orderDate { get; set; }
        public double amount { get; set; }
        public int shopType { get; set; }
    }
}
