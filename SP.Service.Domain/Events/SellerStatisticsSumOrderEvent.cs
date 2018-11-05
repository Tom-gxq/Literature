using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
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
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.SSID = ssid;
            this.OrderAmount = orderAmount;
            this.EventType = EventType.SellerStatisticsSumOrder;
        }
    }
}
