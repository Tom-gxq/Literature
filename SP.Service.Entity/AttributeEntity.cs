using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_Attribute")]
    public class AttributeEntity : BaseEntity
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string AttributeName { get; set; }

        public int? DisplaySequence { get; set; }

        public string UseAttributeImage { get; set; }
        public int? Type { get; set; }
    }
}
