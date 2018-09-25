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
    public class SellerStatisticsDomain : AggregateRoot<Guid>, IHandle<SellerStatisticsEvent>, IHandle<SellerStatisticsTradeEvent>,
        IHandle<SellerStatisticsSumOrderEvent>,IOriginator
    {
        public string ShippingId { get; set; }
        public string Shipto { get; set; }
        public string OrderId { get; set; }
        public double OrderAmount { get; set; }
        public int NewOrder { get; set; }
        public string SSID { get; set; }
        public SellerStatisticsDomain()
        {

        }
        public SellerStatisticsDomain(Guid id, string shippingId, string shipto, string orderId, double orderAmount, DateTime createTime)
        {

            ApplyChange(new SellerStatisticsEvent(id, createTime, shippingId, 1, orderAmount));
            ApplyChange(new SellerStatisticsTradeEvent(id.ToString(), shipto, orderId));
        }

        public void SumOrderStatistics(string ssid, string shipto, string orderId, double orderAmount)
        {
            ApplyChange(new SellerStatisticsSumOrderEvent(ssid, orderAmount));
            ApplyChange(new SellerStatisticsTradeEvent(ssid, shipto, orderId));
        }

        public void Handle(SellerStatisticsEvent e)
        {
            this.OrderAmount = e.OrderAmount;
            this.ShippingId = e.ShippingId;
            this.NewOrder = e.NewOrder;
        }
        public void Handle(SellerStatisticsTradeEvent e)
        {
            this.Shipto = e.ShipTo;
            this.OrderId = e.OrderId;
        }
        public void Handle(SellerStatisticsSumOrderEvent e)
        {
            this.OrderAmount = e.OrderAmount;
            this.SSID = e.AggregateId.ToString();
        }
        public BaseEntity GetMemento()
        {
            return new SellerStatisticsEntity()
            {
                AccountId = this.ShippingId,
                Num_OrderAmount = this.OrderAmount
            };
        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is SellerStatisticsEntity)
            {
                var entity = memento as SellerStatisticsEntity;
                this.ShippingId = entity.AccountId;
                this.OrderAmount = entity.Num_OrderAmount != null ? entity.Num_OrderAmount.Value:0;
                this.NewOrder = entity.Num_NewOrder != null ? entity.Num_NewOrder.Value : 0;
                this.Id = new Guid(entity.SSID);
                this.SSID = entity.SSID;
            }
        }
    }
}
