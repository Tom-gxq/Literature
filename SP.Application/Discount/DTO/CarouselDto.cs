using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Discount.DTO
{
    public class CarouselDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int DisplaySequence { get; set; }
    }
}
