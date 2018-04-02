using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Product
{
    public class ProductAttributeModel
    {
        public AttributeValueModel value { get; set; }
        public AttributeModel attribute { get; set; }
    }
}
