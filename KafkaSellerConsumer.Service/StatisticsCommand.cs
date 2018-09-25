using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Commands.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace KafkaSellerConsumer.Service
{
    public class StatisticsCommand: SumSellerStatisticsCommand
    {
        public string EventType { get; set; }
        public StatisticsCommand(string shippingId, string shipto, string orderId, double orderAmount)
            :base( shippingId,  shipto,  orderId,  orderAmount)
        {

        }
    }
}
