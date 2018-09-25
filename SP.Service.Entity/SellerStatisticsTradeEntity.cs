using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_SellerStatisticsTrade")]
    public class SellerStatisticsTradeEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string SSID { get; set; }
        public string OrderId { get; set; }
        public string ShipTo { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
