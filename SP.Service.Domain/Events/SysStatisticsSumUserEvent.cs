using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsSumUserEvent : Event
    {
        public string AccountId { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public SysStatisticsSumUserEvent(string accountId,DateTime createTime) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.AccountId = accountId;
            this.CreateTime = createTime;
            this.EventType = EventType.SysStatisticsSumUser;
        }
    }
}
