using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Seller
{
    public class ProductTypeModel
    {
        public long TypeId { get; set; }
        public string TypeName { get; set; }
        public int Kind { get; set; }
        public string Remark { get; set; }
        public string TypePath { get; set; }
        public string TypeLogo { get; set; }
    }
}
