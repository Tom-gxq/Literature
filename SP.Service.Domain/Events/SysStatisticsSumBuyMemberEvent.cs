using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class SysStatisticsSumBuyMemberEvent: SysStatisticsSumNewMemberEvent
    {
        public SysStatisticsSumBuyMemberEvent(string accountId, double amount, DateTime createTime) :base(accountId, amount, createTime)
        {
           
        }
    }
}
