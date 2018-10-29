using Grpc.Service.Core.Domain.Events;
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
        public double FoodAmount { get; set; }
        public double MarkAmount { get; set; }
        public int AddressId { get; set; }

        public OrderStatisticsSumEvent(string orderId, string orderCode, string accountId, double foodAmount, double markAmount, int addressId, DateTime orderDate)
        {
            this.OrderId = orderId;
            this.OrderCode = orderCode;
            this.OrderDate = orderDate;
            this.AccountId = accountId;
            this.FoodAmount = foodAmount;
            this.MarkAmount = markAmount;
            this.AddressId = addressId;
        }
    }
}
