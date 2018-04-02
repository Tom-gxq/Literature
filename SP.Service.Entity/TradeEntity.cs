using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_Trade")]
    public class TradeEntity: BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string AccountId { get; set; }
        public string TradeId { get; set; }
        public string CartId { get; set; }
        public int? Subject { get; set; }
        public double? Amount { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
