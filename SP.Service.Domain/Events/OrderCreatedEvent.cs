using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Domain.Events;
using SP.Data.Enum;
using SP.Service.Domain.Util;

namespace SP.Service.Domain.Events
{
    public class OrderCreatedEvent : Event
    {
        public string Remark { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public string AccountId { get; set; }
        public double Amount { get; set; }
        public double VIPAmount { get; set; }
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public bool IsVip { get; set; }
        public int OrderType { get; set; }

        public OrderCreatedEvent(Guid aggregateId, string remark, OrderStatus orderStatus, 
            DateTime orderDate, string accountId, double amount, double vipAmount, int addressId, 
            string address,string mobile, bool isvip,int orderType=0)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            CommandId = aggregateId.ToString();
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
            OrderType = orderType;
            EventType = EventType.OrderCreated;
        }
    }
}
