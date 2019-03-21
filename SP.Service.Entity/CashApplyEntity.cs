using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_CashApply")]
    public class CashApplyEntity : TradeBaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }        
        public string Alipay { get; set; }
        public double Money { get; set; }        
        public int? Status { get; set; }
        public DateTime? UpdateTime { get; set; }
        public CashApplyEntity():base(1)
        {

        }
    }
}
