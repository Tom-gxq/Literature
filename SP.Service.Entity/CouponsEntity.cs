using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_Coupons")]
    public class CouponsEntity : BaseEntity
    {
        public string CouponId { get; set; }
        public string KindId { get; set; }
        public string AssociatorId { get; set; }
        public string AccountId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
    }
}
