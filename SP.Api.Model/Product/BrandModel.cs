using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Product
{
    public class BrandModel
    {
        public int brandId { get; set; }
        public string brandName { get; set; }
        public string logo { get; set; }
        public string companyUrl { get; set; }
        public string description { get; set; }
    }
}
