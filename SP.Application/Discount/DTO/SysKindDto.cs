using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Discount.DTO
{
    public class SysKindDto
    {
        public int Num { get; set; }
        public string KindId { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double DiscountValue { get; set; }
    }
}
