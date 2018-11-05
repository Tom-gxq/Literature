using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class OrderStatisticsSumEvent : Event
    {
        public string OrderId { get; set; }

        public string OrderCode { get; set; }

        public DateTime OrderDate { get; set; }

        public string AccountId { get; set; }
        public double Amount { get; set; }
        public int AddressId { get; set; }

        public OrderStatisticsSumEvent(string orderId, string orderCode, string accountId, double amount, int addressId, DateTime orderDate)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            this.OrderId = orderId;
            this.OrderCode = orderCode;
            this.OrderDate = orderDate;
            this.AccountId = accountId;
            this.Amount = amount;
            this.AddressId = addressId;
            this.EventType = EventType.OrderStatisticsSum;
        }
    }
}
