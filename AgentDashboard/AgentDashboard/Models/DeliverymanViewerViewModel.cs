using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class DeliverymanViewerViewModel
    {
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Phone { get; set; }
        public string Region { get; set; }
        public string Dorm { get; set; }
        public decimal Amount { get; set; }
    }
}