using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class ProductAttributeDto
    {
        public string ProductId { get; set; }
        public long? AttributeId { get; set; }
        public long? ValueId { get; set; }
    }
}
