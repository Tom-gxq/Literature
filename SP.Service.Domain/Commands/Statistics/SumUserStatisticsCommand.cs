using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Statistics
{
    public class SumUserStatisticsCommand : Command
    {
        public string AccountId { get;  set; }
        public DateTime CreateTime { get;  set; }
        public SumUserStatisticsCommand(string accountId, DateTime createTime) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.AccountId = accountId;
            this.CreateTime = createTime;
            this.CommandType = CommandType.SumUserStatistics;
        }
    }
}
