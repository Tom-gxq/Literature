using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class AttributeDto
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        public string UseAttributeImage { get; set; }
        public int DisplaySequence { get; set; }
    }
}
