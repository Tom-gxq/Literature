using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsSumUserEvent : Event
    {
        public string AccountId { get; set; }
        public DateTime CreateTime { get; set; }
        public SysStatisticsSumUserEvent(Guid id,string accountId,DateTime createTime) : base(KafkaConfig.EventBusTopicTitle)
        {
            this.CommandId = id.ToString();
            this.AccountId = accountId;
            this.CreateTime = createTime;
            this.EventType = EventType.SysStatisticsSumUser;
        }
    }
}
