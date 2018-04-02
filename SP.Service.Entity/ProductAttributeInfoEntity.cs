using Grpc.Service.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    public class ProductAttributeInfoEntity : BaseEntity
    {
        public string ProductId { get; set; }
        public long? AttributeId { get; set; }
        public long? ValueId { get; set; }
        public string AttributeName { get; set; }
        public string ValueStr { get; set; }
    }
}
