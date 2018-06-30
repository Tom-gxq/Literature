using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Events;
using SP.Data.Enum;

namespace SP.Service.Domain.Events
{
    public class OrderCreatedEvent : Event
    {
        public string Remark { get; internal set; }
        public OrderStatus OrderStatus { get; internal set; }
        public DateTime OrderDate { get; internal set; }
        public string AccountId { get; internal set; }
        public double Amount { get; internal set; }
        public double VIPAmount { get; internal set; }
        public int AddressId { get; internal set; }
        public string Address { get; internal set; }
        public string Mobile { get; internal set; }
        public bool IsVip { get; internal set; }

        public OrderCreatedEvent(Guid aggregateId, string remark, OrderStatus orderStatus, DateTime orderDate, string accountId, double amount, double vipAmount, int addressId, string address,string mobile, bool isvip)
        {
            AggregateId = aggregateId;
            Remark = remark;
            OrderStatus = orderStatus;
            OrderDate = orderDate;
            AccountId = accountId;
            Amount = amount;
            VIPAmount = vipAmount;
            AddressId = addressId;
            Address = address;
            Mobile = mobile;
            IsVip = isvip;
        }
    }
}
