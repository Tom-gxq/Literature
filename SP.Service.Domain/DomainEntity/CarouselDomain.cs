using Grpc.Service.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class CarouselDomain : AggregateRoot<Guid>
    {
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public int DisplayIndex { get; set; }
    }
}
