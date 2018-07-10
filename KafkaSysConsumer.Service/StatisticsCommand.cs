using SP.Producer;
using SP.Service.Domain.Commands.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafkaSysConsumer.Service
{
    public class StatisticsCommand: SumMemberStatisticsCommand
    {
        public string EventType { get; set; }
        public StatisticsCommand(string accountId, double amount, DateTime createTime, AuthorizeType type) :base(accountId, amount, createTime, type)
        {

        }
    }
}
