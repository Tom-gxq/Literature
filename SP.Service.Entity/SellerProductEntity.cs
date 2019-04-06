using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_SellerProduct")]
    public class SellerProductEntity : BaseEntity
    {
        public int? SupplierProductId { get; set; }
        public string AccountId { get; set; }
        public int? PreStock { get; set; }
    }
}
