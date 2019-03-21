using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_AccountFinance")]
    public class AccountFinanceEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string AccountId { get; set; }
        public double? HaveAmount { get; set; }
        public double? UseAmount { get; set; }
        public double? ConsumeAmount { get; set; }
    }
}
