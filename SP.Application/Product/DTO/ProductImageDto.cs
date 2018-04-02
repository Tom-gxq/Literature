using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ImgPath { get; set; }
        public int? Postion { get; set; }
    }
}
