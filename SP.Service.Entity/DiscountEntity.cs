using Grpc.Service.Core.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    public class DiscountEntity : BaseEntity
    {
        public string AssociatorId { get; set; }
        public string AccountId { get; set; }
        public int? Quantity { get; set; }
        public double? Amount { get; set; }
        public string PayOrderCode { get; set; }
        public int? PayType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public string Description { get; set; }
        public double? DiscountValue { get; set; }
    }
}
