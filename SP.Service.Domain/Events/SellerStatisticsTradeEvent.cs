using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SellerStatisticsTradeEvent : Event
    {
        public string ShipTo { get; set; }
        public string OrderId { get; set; }
        public string SSID { get; internal set; }
        public SellerStatisticsTradeEvent(string ssid, string shipto, string orderId)
        {
            this.SSID = ssid;
            this.OrderId = orderId;
            this.ShipTo = shipto;
        }

    }
}
