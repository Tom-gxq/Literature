using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class BrandDto
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public string CompanyUrl { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string Description { get; set; }
        public int DisplaySequence { get; set; }


    }
}
