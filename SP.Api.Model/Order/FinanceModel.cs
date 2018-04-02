using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Order
{
    public class FinanceModel
    {
        public string accountId { get; set; }
        public double haveAmount { get; set; }
        public double useAmount { get; set; }
        public double activeAmount { get; set; }
    }
}
