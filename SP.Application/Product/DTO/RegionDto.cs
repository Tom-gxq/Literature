using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class RegionDto
    {
        public int DataId { get; set; }
        public string DataName { get; set; }
        public int ParentDataID { get; set; }
        public string ParentDataName { get; set; }
        public int DataType { get; set; }

        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public int DistrictID { get; set; }
        public string DistrictName { get; set; }
        public int? DisplaySequence { get; set; }
    }
}
