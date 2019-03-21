using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsSumBuyMemberEvent: SysStatisticsSumNewMemberEvent
    {
        public SysStatisticsSumBuyMemberEvent(Guid id,string accountId, double amount, DateTime createTime) :base(id,accountId, amount, createTime)
        {
            this.EventType = Grpc.Service.Core.Domain.Events.EventType.SysStatisticsSumBuyMember;
        }
    }
}
