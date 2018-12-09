using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ConsumeTrade")]
    public class ConsumeTradeEntity : TradeBaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string TradeId { get; set; }
        public double? Amount { get; set; }
        public string OrderId { get; set; }
        public string CheckCode { get; set; }
        public ConsumeTradeEntity():base(2)
        {

        }
    }
}
