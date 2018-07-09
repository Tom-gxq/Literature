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
    public class ShipOrderDomain : AggregateRoot<Guid>, IHandle<ShipOrderCreatedEvent>, IOriginator
    {
        public string OrderId { get; internal set; }
        public string ShippingId { get; internal set; }
        public string ShipTo { get; internal set; }
        public DateTime ShippingDate { get; internal set; }
        public int Stock { get; internal set; }
        public string ProductId { get; internal set; }
        public int ShopId { get; internal set; }
        public ShipOrderDomain()
        {

        }
        public ShipOrderDomain(string orderId, string shippingId, string shipTo, DateTime shippingDate, int stock, string productId,int shopId)
        {
            ApplyChange(new ShipOrderCreatedEvent(orderId, shippingId, shipTo, shippingDate, stock, productId, shopId));
        }

        public BaseEntity GetMemento()
        {
            return new ShippingOrdersEntity()
            {
                OrderId = this.OrderId,
                ShippingId = this.ShippingId,
                ShipTo = this.ShipTo,
                ShippingDate = this.ShippingDate,
                Stock = this.Stock,
                ProductId = this.ProductId
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is ShippingOrdersEntity)
            {
                var entity = memento as ShippingOrdersEntity;
                this.OrderId = entity.OrderId;
                this.ShippingId = entity.ShippingId;
                this.ShipTo = entity.ShipTo;
                this.ShippingDate = entity.ShippingDate.Value;
                this.Stock = entity.Stock.Value;
                this.ProductId = entity.ProductId;
            }
        }
        public void Handle(ShipOrderCreatedEvent e)
        {
            this.OrderId = e.OrderId;
            this.ShippingId = e.ShippingId;
            this.ShipTo = e.ShipTo;
            this.ShippingDate = e.ShippingDate;
            this.Stock = e.Stock;
            this.ProductId = e.ProductId;
        }
    }
}
