using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsSumNewMemberEvent:Event
    {
        public string AccountId { get; internal set; }
        public double Amount { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public SysStatisticsSumNewMemberEvent(string accountId,double amount, DateTime createTime)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.AccountId = accountId;
            this.Amount = amount;
            this.CreateTime = createTime;
            this.EventType = EventType.SysStatisticsSumNewMember;
        }
    }
}
