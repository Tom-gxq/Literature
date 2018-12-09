using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SellerStatisticsSumOrderEvent:Event
    {
        public double OrderAmount { get; set; }
        public string SSID { get; set; }
        public SellerStatisticsSumOrderEvent(Guid id,string ssid, double orderAmount)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.SSID = ssid;
            this.OrderAmount = orderAmount;
            this.EventType = EventType.SellerStatisticsSumOrder;
        }
    }
}
