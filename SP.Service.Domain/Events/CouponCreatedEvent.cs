using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class CouponCreatedEvent : Event
    {
        public string KindId { get; set; }
        public string AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double ModelAmount { get; set; }
        public string ModeDescription { get; set; }
        public string PayOrderCode { get; set; }
        public int PayType { get; set; }
        public int PayStatus { get; set; }

        public CouponCreatedEvent(Guid aggregateId,  string kindId, string accountId, DateTime startDate, DateTime endDate, string payOrderCode)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            CommandId = aggregateId.ToString();
            KindId = kindId;
            AccountId = accountId;
            StartDate = startDate;
            EndDate = endDate;
            PayOrderCode = payOrderCode;
            EventType = EventType.CouponCreated;
        }
    }
}
