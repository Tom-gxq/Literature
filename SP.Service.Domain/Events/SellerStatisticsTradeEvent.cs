﻿using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SellerStatisticsTradeEvent : Event
    {
        public string ShipTo { get; set; }
        public string OrderId { get; set; }
        public string SSID { get; set; }
        public SellerStatisticsTradeEvent(Guid id, string ssid, string shipto, string orderId)
             : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.SSID = ssid;
            this.OrderId = orderId;
            this.ShipTo = shipto;
            this.EventType = EventType.SellerStatisticsTrade;
        }

    }
}
