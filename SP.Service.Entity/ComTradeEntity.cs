using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_IncomeTrade")]
    public class ComTradeEntity : TradeBaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string TradeId { get; set; }
        public string ProductId { get; set; }
        public int? Subject { get; set; }
        public double? Amount { get; set; }
        public int? ShipOrderId { get; set; }
        public ComTradeEntity() : base(0)
        {

        }
    }
}
