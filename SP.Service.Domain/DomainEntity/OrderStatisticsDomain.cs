using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class OrderStatisticsDomain : AggregateRoot<Guid>, IHandle<OrderStatisticsSumEvent>,
        IHandle<OrderStatisticsCreateEvent>,IOriginator
    {
        public string OrderId { get; internal set; }

        public string OrderCode { get; internal set; }

        public DateTime OrderDate { get; internal set; }

        public string AccountId { get; internal set; }
        public double Amount { get; internal set; }
        public int AddressId { get; internal set; }

        public OrderStatisticsDomain()
        {

        }
        public OrderStatisticsDomain(string orderId, string orderCode, DateTime orderDate, string accountId,
            double amount, int addressId)
        {
            ApplyChange(new OrderStatisticsCreateEvent(orderId, orderCode, accountId, amount, addressId, orderDate));
        }
        public void SumOrderStatistics(string orderId, string orderCode, DateTime orderDate, string accountId,
            double amount, int addressId)
        {
            ApplyChange(new OrderStatisticsSumEvent(orderId, orderCode, accountId, amount, addressId, orderDate));
        }
        public void Handle(OrderStatisticsSumEvent e)
        {
            this.OrderId = e.OrderId;
            this.OrderCode = e.OrderCode;
            this.Amount = e.Amount;
            this.OrderDate = e.OrderDate;
            this.AccountId = e.AccountId;
            this.AddressId = e.AddressId;
        }
        public void Handle(OrderStatisticsCreateEvent e)
        {
            this.OrderId = e.OrderId;
            this.OrderCode = e.OrderCode;
            this.Amount = e.Amount;
            this.OrderDate = e.OrderDate;
            this.AccountId = e.AccountId;
            this.AddressId = e.AddressId;
        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is ShipStatisticsEntity)
            {
                var order = memento as ShipStatisticsEntity;
                this.Amount = order.Num_OrderAmount.Value;
                this.OrderDate = order.CreateTime.Value;
                this.AccountId = order.AccountId;
                this.AddressId = order.DormId.Value;
            }
        }
        public BaseEntity GetMemento()
        {
            return new ShipStatisticsEntity()
            {

            };
        }
    }
}
