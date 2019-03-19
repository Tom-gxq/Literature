using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    public class CouponsFullEntity: CouponsEntity
    {
        public string Description { get; set; }
        public double? Amount { get; set; }
        public double? ModelAmount { get; set; }
        public string ModeDescription { get; set; }
    }
}
