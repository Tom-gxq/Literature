using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class AssociatorModel
    {
        public string associatorId { get; set; }
        public string accountId { get; set; }
        public string kindId { get; set; }
        public int quantity { get; set; }
        public string payOrderCode { get; set; }
        public int payType { get; set; }
        public int status { get; set; }
        public double amount { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}
