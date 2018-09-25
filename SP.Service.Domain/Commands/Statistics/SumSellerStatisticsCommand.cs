using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Statistics
{
    public class SumSellerStatisticsCommand: Command
    {
        public string ShippingId { get; set; }
        public string Shipto { get; set; }
        public string OrderId { get; set; }
        public double OrderAmount { get; set; }
        public SumSellerStatisticsCommand(string shippingId, string shipto, string orderId, double orderAmount)
        {
            this.ShippingId = shippingId;
            this.Shipto = shipto;
            this.OrderId = orderId;
            this.OrderAmount = orderAmount;
        }
    }
}
