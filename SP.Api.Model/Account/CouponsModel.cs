using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class CouponsModel
    {
        public string CouponId { get; set; }
        public string Description { get; set; }
        public string ModeDescription { get; set; }
        public long StartDate { get; set; }
        public long EndDate { get; set; }
        public int Status { get; set; }
        public double Amount { get; set; }
        public double ModeAmount { get; set; }
    }
}
