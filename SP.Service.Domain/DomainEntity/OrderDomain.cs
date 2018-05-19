
using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Data.Enum;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class OrderDomain : AggregateRoot<Guid>,
        IHandle<OrderCreatedEvent>, IHandle<UpdateShoppingCartOrderIDEvent>,
        IOriginator
    {
        public string OrderId { get; internal set; }
        public string Remark { get; internal set; }
        public OrderStatus OrderStatus { get; internal set; }
        public string CloseReason { get; internal set; }
        public DateTime OrderDate { get; internal set; }
        public DateTime PayDate { get; internal set; }
        public DateTime ShippingDate { get; internal set; }
        public DateTime FinishDate { get; internal set; }
        public string AccountId { get; internal set; }
        public DateTime ShipToDate { get; internal set; }
        public long Freight { get; internal set; }
        public double Amount { get; internal set; }
        public double VIPAmount { get; internal set; }
        public string OrderCode { get; internal set; }
        public List<ProductDomain> Products { get; internal set; }
        public int AdressId { get; internal set; }

        public OrderDomain()
        {

        }

        public OrderDomain(Guid id, string remark, OrderStatus orderStatus, DateTime orderDate, string accountId, List<ShoppingCartsDomain> shoppingCarts, int addressId,string address)
        {
            SumOrderAmount(shoppingCarts,id);
            ApplyChange(new OrderCreatedEvent(id, remark, orderStatus, orderDate, accountId, this.Amount,this.VIPAmount, addressId, address));
        }

        private void SumOrderAmount(List<ShoppingCartsDomain> shoppingCarts, Guid orderId)
        {
            this.Amount = 0;
            this.VIPAmount = 0;
            foreach (var item in shoppingCarts)
            {
                if (item.Product != null && item.Product.MarketPrice != null)
                {
                    this.Amount += item.Quantity * item.Product.MarketPrice.Value;                    
                }
                if (item.Product != null && item.Product.VIPPrice != null)
                {
                    this.VIPAmount += item.Quantity * item.Product.VIPPrice.Value;
                }
                ApplyChange(new UpdateShoppingCartOrderIDEvent(item.CartId, orderId.ToString()));
            }
        }

        public void EditOrderDomainStatus(Guid id, OrderStatus orderStatus)
        {
            ApplyChange(new OrderEditEvent(id, orderStatus));
        }

        public void Handle(OrderCreatedEvent e)
        {
            this.OrderId = e.AggregateId.ToString();
            this.Remark = e.Remark;
            this.OrderStatus = e.OrderStatus;
            this.OrderDate = e.OrderDate;
            this.AccountId = e.AccountId;
            this.AdressId = e.AddressId;
        }

        public void Handle(OrderEditEvent e)
        {
            this.OrderId = e.CommandId;
            this.OrderStatus = e.OrderStatus;
        }

        public void Handle(UpdateShoppingCartOrderIDEvent e)
        {
            
        }

        public BaseEntity GetMemento()
        {
            return new OrdersEntity()
            {
                OrderId = this.OrderId,
                OrderDate = this.OrderDate,
                AccountId = this.AccountId,
                OrderStatus = (int)this.OrderStatus,
                UpdateTime = DateTime.Now,
                Remark = this.Remark,
                AddressId = this.AdressId
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is OrdersEntity)
            {
                var order = memento as OrdersEntity;
                this.OrderId = order.OrderId;
                this.Remark = order.Remark;
                this.OrderStatus = (OrderStatus)order.OrderStatus;
                this.CloseReason = order.CloseReason;
                this.OrderDate = order.OrderDate.Value;
                this.PayDate = order.PayDate != null ? order.PayDate.Value : DateTime.MinValue;
                this.ShippingDate = order.ShippingDate != null ? order.ShippingDate.Value : DateTime.MinValue;
                this.FinishDate = order.FinishDate != null ? order.FinishDate.Value : DateTime.MinValue;
                this.AccountId = order.AccountId;
                this.ShipToDate = order.ShipToDate != null ? order.ShipToDate.Value : DateTime.MinValue;
                this.Freight = order.Freight != null ? order.Freight.Value:0;
                this.Amount = order.Amount.Value;
                this.VIPAmount = order.VIPAmount != null? order.VIPAmount.Value:0;
                this.OrderCode = order.OrderCode!= null? order.OrderCode:string.Empty ;
                this.AdressId = order.AddressId != null ? order.AddressId.Value : 0;
            }
        }

        public void SetMemenProductto(List<ProductDomain> memento)
        {
            if (memento != null)
            {
                this.Products = memento;
            }
        }
    }
}
