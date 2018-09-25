using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User.DTO
{
    public class AssociatorDto
    {
        public string AssociatorId { get; set; }
        public string AccountId { get; set; }
        public string KindId { get; set; }
        public int Quantity { get; set; }
        public string PayOrderCode { get; set; }
        public int PayType { get; set; }
        public int Status { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string KindName { get; set; }
        public string FullName { get; set; }
    }
}
