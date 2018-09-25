using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Chart.DTO
{
    public class SellerStatisticsDto
    {
        public string SSID { get; set; }
        public string AccountId { get; set; }
        public int NewOrder { get; set; }
        public double OrderAmount { get; set; }
        public bool IsChecked { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
