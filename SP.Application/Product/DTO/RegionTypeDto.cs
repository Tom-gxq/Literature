using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class RegionTypeDto
    {
        public int Id { get; set; }
        public int RegionId { get; set; }
        public int TypeId { get; set; }
        public string DataName { get; set; }
        public string TypeName { get; set; }
        public int DisplaySequence { get; set; }
    }
}
