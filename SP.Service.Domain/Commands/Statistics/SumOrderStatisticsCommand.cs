using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Statistics
{
    public class SumOrderStatisticsCommand : Command
    {
        public string OrderId { get; set; }

        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public string AccountId { get; set; }
        public double Amount { get; set; }
        public int AddressId { get; set; }

        public SumOrderStatisticsCommand(string orderId, string orderCode, string accountId, double amount, int addressId, DateTime orderDate)
            : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.OrderId = orderId;
            this.OrderCode = orderCode;
            this.OrderDate = orderDate;
            this.AccountId = accountId;
            this.Amount = amount;
            this.AddressId = addressId;
            this.CommandType = CommandType.SumOrderStatistics;
        }
    }
}
