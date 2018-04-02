using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_Carousel")]
    public class CarouselEntity : Entity
    {
        [AutoIncrement]
        public override int Id { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int DisplaySequence { get; set; }
    }
}
