using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Order
{
    public class CouponsModel
    {
        public string CouponId { get; set; }
        public string KindId { get; set; }
        public string AssociatorId { get; set; }
        public string AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double ModelAmount { get; set; }
        public string ModeDescription { get; set; }
    }
}
