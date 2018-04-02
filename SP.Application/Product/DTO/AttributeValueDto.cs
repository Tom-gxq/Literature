using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class AttributeValueDto
    {
        public long ValueId { get; set; }
        public int AttributeId { get; set; }
        public int DisplaySequence { get; set; }
        public string ValueStr { get; set; }
        public string ImageUrl { get; set; }
    }
}
