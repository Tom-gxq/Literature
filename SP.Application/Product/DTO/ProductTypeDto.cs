using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class ProductTypeDto
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int Kind { get; set; }
        public string Remark { get; set; }
        public int DisplaySequence { get; set; }
        public string TypePath { get; set; }
        public string TypeLogo { get; set; }
    }
}
