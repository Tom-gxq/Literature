using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    public class TradeBaseEntity: BaseEntity
    {
        public string AccountId { get; set; }
        public DateTime? CreateTime { get; set; }
        public double BalanceAmount { get; set; }
        public string TradeNo { get; set; }  
        [Ignore]
        public int TradeType { get; set; }
        public TradeBaseEntity(int tradeType)
        {
            TradeType = tradeType;
        }
    }
}
