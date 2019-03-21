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
        public OrderStatisticsDomain(Guid id,string orderId, string orderCode, DateTime orderDate, string accountId,
            double foodAmount, double markAmount, int addressId)
        {
            ApplyChange(new OrderStatisticsCreateEvent(id,orderId, orderCode, accountId, foodAmount, markAmount, addressId, orderDate));
        }
        public void SumOrderStatistics(Guid id,string orderId, string orderCode, DateTime orderDate, string accountId,
            double foodAmount, double markAmount, int addressId)
        {
            ApplyChange(new OrderStatisticsSumEvent(id,orderId, orderCode, accountId, foodAmount, markAmount, addressId, orderDate));
        }
        public void Handle(OrderStatisticsSumEvent e)
        {
            this.OrderId = e.OrderId;
            this.OrderCode = e.OrderCode;
            this.Amount = e.FoodAmount + e.MarkAmount;
            this.OrderDate = e.OrderDate;
            this.AccountId = e.AccountId;
            this.AddressId = e.AddressId;
        }
        public void Handle(OrderStatisticsCreateEvent e)
        {
            this.OrderId = e.OrderId;
            this.OrderCode = e.OrderCode;
            this.Amount = e.FoodAmount+e.MarkAmount;
            this.OrderDate = e.OrderDate;
            this.AccountId = e.AccountId;
            this.AddressId = e.AddressId;
        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is ShipStatisticsEntity)
            {
                var order = memento as ShipStatisticsEntity;
                this.Amount = order.Num_FoodOrderAmount.Value+ order.Num_MarkOrderAmount.Value;
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
