﻿using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_SellerStatistics")]
    public class SellerStatisticsEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string SSID { get; set; }
        public string AccountId { get; set; }
        public int? Num_NewOrder { get; set; }
        public double? Num_OrderAmount { get; set; }
        public bool? IsChecked { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
