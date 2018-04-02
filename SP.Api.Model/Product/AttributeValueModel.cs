using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Product
{
    public class AttributeValueModel
    {
        public long valueId { get; set; }
        /**
        * 商品类型属性值
        */
        public long attributeId { get; set; }
        /**
        * 
        */
        public string valueStr { get; set; }
    }
}
