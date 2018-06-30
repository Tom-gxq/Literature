using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Statistics
{
    public class SumUserStatisticsCommand : Command
    {
        public string AccountId { get;  set; }
        public DateTime CreateTime { get;  set; }
        public SumUserStatisticsCommand(string accountId, DateTime createTime)
        {
            this.AccountId = accountId;
            this.CreateTime = createTime;
        }
    }
}
