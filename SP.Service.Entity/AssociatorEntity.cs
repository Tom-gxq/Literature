using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_Associator")]
    public class AssociatorEntity : BaseEntity
    {
        [AutoIncrement]
        public  int? Id { get; set; }
        public string AssociatorId { get; set; }
        public string AccountId { get; set; }
        public string KindId { get; set; }
        public int? Quantity { get; set; }
        public double? Amount { get; set; }
        public string PayOrderCode { get; set; }
        public int? PayType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
    }
}
