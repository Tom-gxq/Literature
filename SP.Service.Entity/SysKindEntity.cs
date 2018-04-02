using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_SysKind")]
    public class SysKindEntity : BaseEntity
    {
        public string KindId { get; set; }
        public int? Kind { get; set; }
        public int? Quantity { get; set; }
        public int? Unit { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public double? DiscountValue { get; set; }
    }
}
