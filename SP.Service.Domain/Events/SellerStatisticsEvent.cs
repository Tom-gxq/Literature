using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SellerStatisticsEvent : Event
    {
        public string ShippingId { get; set; }
        public int NewOrder { get; internal set; }
        public double OrderAmount { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public SellerStatisticsEvent(Guid id,DateTime createTime, string shippingId,int newOrder, double orderAmount)
        {
            this.AggregateId = id;
            this.CreateTime = createTime;
            this.NewOrder = newOrder;
            this.ShippingId = shippingId;
            this.OrderAmount = orderAmount;
        }
    }
}
