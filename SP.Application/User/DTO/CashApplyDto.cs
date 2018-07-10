using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User.DTO
{
    public class CashApplyDto
    {
        public int Id { get; set; }
        public AccountInfoDto AccountInfo { get; set; }
        public string Alipay { get; set; }
        public double Money { get; set; }
        public DateTime CreateTime { get; set; }
        public double HaveAmount { get; set; }
        public int Status { get; set; }
    }
}
