using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_SuppliersProduct")]
    public class SuppliersProductEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public int? SuppliersId { get; set; }
        public string ProductId { get; set; }
        public double? PurchasePrice { get; set; }
        public int? Status { get; set; }
        public int? AlertStock { get; set; }
        public int? SaleStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
