using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AssociatorCreatedEvent : Event
    {
        public string AccountId { get; set; }
        public string KindId { get; set; }
        public int Quantity { get; set; }
        public string PayOrderCode { get; set; }
        public int PayType { get; set; }
        public double Amount { get; set; }
        public int Status { get; set; }
        public AssociatorCreatedEvent(Guid associatorId,string accountId, string kindId,  int quantity,
            string payOrderCode, int payType, double amount,int status)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = associatorId;
            CommandId = associatorId.ToString();
            KindId = kindId;
            AccountId = accountId;
            Quantity = quantity;
            PayOrderCode = payOrderCode;
            PayType = payType;
            Amount = amount;
            Status = status;
            EventType = EventType.AssociatorCreated;
        }
    }
}
