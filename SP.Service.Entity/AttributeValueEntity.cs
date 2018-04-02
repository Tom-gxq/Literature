using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_AttributeValue")]
    public class AttributeValueEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public int? AttributeId { get; set; }
        public int? DisplaySequence { get; set; }
        public string ValueStr { get; set; }
        public string ImageUrl { get; set; }
    }
}
