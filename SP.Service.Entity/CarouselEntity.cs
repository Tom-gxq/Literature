using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_Carousel")]
    public class CarouselEntity: BaseEntity
    {
        [AutoIncrement]
        public  int Id { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int DisplaySequence { get; set; }
    }
}
