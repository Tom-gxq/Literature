using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_SaleMode")]
    public class SaleModeEntity : BaseEntity
    {
        public string SaleModeId { get; set; }
        public int? Type { get; set; }
        public double? ModelAmount { get; set; }
        public string ModeDescription { get; set; }
    }
}
