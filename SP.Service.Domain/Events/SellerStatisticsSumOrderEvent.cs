using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SellerStatisticsSumOrderEvent:Event
    {
        public double OrderAmount { get; internal set; }
        public string SSID { get; internal set; }
        public SellerStatisticsSumOrderEvent(string ssid, double orderAmount)
        {
            this.SSID = ssid;
            this.OrderAmount = orderAmount;
        }
    }
}
