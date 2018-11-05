using Grpc.Service.Core.Domain.Commands;
using SP.Producer;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Statistics
{
    public class SumMemberStatisticsCommand : SumUserStatisticsCommand
    {
        public double Amount { get;  set; }
        public AuthorizeType Type { get;  set; }

        public SumMemberStatisticsCommand(string accountId,double amount, DateTime createTime, AuthorizeType type):base(accountId, createTime)
        {
            this.Amount = amount;
            this.Type = type;
            this.CommandType = CommandType.SumMemberStatistics;
        }
    }
    
}
