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
    public class ShipOrderDomain : AggregateRoot<Guid>, IHandle<ShipOrderCreatedEvent>, IHandle<EditShipOrderStatusEvent>,IOriginator
    {
        public int ShipOrderId { get; internal set; }
        public string OrderId { get; internal set; }
        public string ShippingId { get; internal set; }
        public string ShipTo { get; internal set; }
        public DateTime ShippingDate { get; internal set; }
        public int Stock { get; internal set; }
        public string ProductId { get; internal set; }
        public int ShopId { get; internal set; }
        public string OrderAddress { get; set; }
        public bool IsVip { get; set; }
        public bool IsWxPay { get; set; }
        public bool IsAliPay { get; set; }
        public int OrderType { get; set; }
        public string ProductName { get; set; }
        public double MarketPrice { get; set; }
        public double VIPPrice { get; set; }
        public double PurchasePrice { get; set; }
        public long SecondTypeId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PayDate { get; set; }
        public ShipOrderDomain()
        {

        }
        public ShipOrderDomain(string orderId, string shippingId, string shipTo, DateTime shippingDate, int stock, string productId,int shopId)
        {
            ApplyChange(new ShipOrderCreatedEvent(orderId, shippingId, shipTo, shippingDate, stock, productId, shopId));
        }
        public void EditShipOrderDomainStatus(List<int> shipOrderId,bool isShiped)
        {
            foreach (var item in shipOrderId)
            {
                ApplyChange(new EditShipOrderStatusEvent(item, isShiped));
            }
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
                this.ShipOrderId = entity.Id.Value;
                this.OrderId = entity.OrderId;
                this.ShippingId = entity.ShippingId;
                this.ShipTo = entity.ShipTo;
                this.ShippingDate = entity.ShippingDate.Value;
                this.Stock = entity.Stock.Value;
                this.ProductId = entity.ProductId;
            }
            if (memento is ShippingOrderFullEntity)
            {
                var entity = memento as ShippingOrderFullEntity;
                this.OrderAddress = entity.OrderAddress;
                this.IsVip = entity.IsVip != null ? entity.IsVip.Value:false;
                this.IsWxPay = entity.IsWxPay != null ? entity.IsWxPay.Value : false;
                this.IsAliPay = entity.IsAliPay != null ? entity.IsAliPay.Value : false;
                this.OrderType = entity.OrderType != null ? entity.OrderType.Value : 0; 
                this.ProductName = entity.ProductName;
                this.MarketPrice = entity.MarketPrice != null ? entity.MarketPrice.Value : 0;
                this.VIPPrice = entity.VIPPrice != null ? entity.VIPPrice.Value : 0;
                this.PurchasePrice = entity.PurchasePrice != null ? entity.PurchasePrice.Value : 0;
                this.SecondTypeId = entity.SecondTypeId != null ? entity.SecondTypeId.Value : 0;
                this.OrderDate = entity.OrderDate.Value;
                this.PayDate = entity.PayDate != null ? entity.PayDate.Value : DateTime.MinValue;
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
        public void Handle(EditShipOrderStatusEvent e)
        {
            this.ShipOrderId = e.ShipOrderId;
           
        }
    }
}
