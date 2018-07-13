using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Order
{
    public class OrderModel
    {
        public string accountId { get; set; }
        public List<string> cartIds { get; set; }
        public string remark { get; set; }
        public string orderId { get; set; }
        public int orderStatus { get; set; }
        public int addressId { get; set; }
        public int orderType { get; set; }
    }
}
