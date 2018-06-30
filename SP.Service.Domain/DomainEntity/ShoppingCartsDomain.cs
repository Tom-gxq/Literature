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
    public class ShoppingCartsDomain:AggregateRoot<Guid>, IHandle<CreatShoppingCartEvent>, IHandle<DelShoppingCartEvent>, IOriginator
    {
        public string CartId { get; internal set; }
        public string AccountId { get; internal set; }
        public int Quantity { get; internal set; }
        public string OrderId { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public DateTime UpdateTime { get; internal set; }
        public ProductEntity Product { get; internal set; }
        public int ShopId { get; set; }
        public double Amount { get; internal set; }
        public double VIPAmount { get; internal set; }

        public ShoppingCartsDomain()
        {
        }

        public ShoppingCartsDomain(Guid id, string accountId,  int quantity, string productId, int shopId,string shiperId)
        {
            ApplyChange(new CreatShoppingCartEvent(accountId, id.ToString(), quantity, productId, DateTime.Now, shopId, shiperId));
        }
        public void DeleteShoppingCart(string cartId, string orderId, string productId, double amount, double vipAmount)
        {
            ApplyChange(new DelShoppingCartEvent(cartId));
            ApplyChange(new OrderSubAmountEvent(new Guid(orderId), productId, amount, vipAmount));
        }
        public void AddShoppingCartNum(Guid id, string accountId, int quantity, string productId, int shopId)
        {
            ApplyChange(new AddShoppingCartNumEvent(accountId, id.ToString(), quantity, productId, DateTime.Now, shopId));
        }
        public void Handle(DelShoppingCartEvent e)
        {
            this.CartId = e.CartId;
        }
        public void Handle(OrderSubAmountEvent e)
        {
            this.CartId = e.AggregateId.ToString();
            this.Amount = e.Amount;
            this.VIPAmount = e.VipAmount;
            this.Product = new ProductEntity() { ProductId = e.ProductId };
        }
        public void Handle(CreatShoppingCartEvent e)
        {
            this.AccountId = e.AccountId.ToString();
            this.CartId = e.CartId;
            this.Product = new ProductEntity() { ProductId = e.ProductId };
            this.Quantity = e.Quantity;
            this.CreateTime = e.CreateTime;
            this.ShopId = e.ShopId;
        }
        public void Handle(AddShoppingCartNumEvent e)
        {
            this.AccountId = e.AccountId.ToString();
            this.CartId = e.CartId;
            this.Product = new ProductEntity() { ProductId = e.ProductId };
            this.Quantity = e.Quantity;
            this.UpdateTime = e.CreateTime;
            this.ShopId = e.ShopId;
        }

        public BaseEntity GetMemento()
        {
            return new ShoppingCartsEntity()
            {
                CartId = this.CartId,                
                AccountId = this.AccountId,
                Quantity = this.Quantity,
                ProductId = this.Product.ProductId,
                CreateTime = this.CreateTime,
                UpdateTime = this.UpdateTime,
                ShopId = this.ShopId
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is ShoppingCartsEntity)
            {
                var cart = memento as ShoppingCartsEntity;
                this.CartId = cart.CartId;
                this.AccountId = cart.AccountId;
                this.CreateTime = cart.CreateTime.Value;
                this.UpdateTime = cart.UpdateTime.Value;
                this.Quantity = cart.Quantity != null ? cart.Quantity.Value : 0;
                this.Product = new ProductEntity() { ProductId = cart.ProductId };
                this.ShopId = cart.ShopId != null ? cart.ShopId.Value:0;
            }
        }
        public void SetMemenProductto(BaseEntity memento)
        {
            if (memento is ProductEntity)
            {
                this.Product = memento as ProductEntity;
            }
        }
        public void CalculateAmount()
        {
            if (this.Product != null && this.Product.MarketPrice != null)
            {
                this.Amount = this.Product.MarketPrice.Value * this.Quantity;
            }
            if (this.Product != null && this.Product.VIPPrice != null)
            {
                this.VIPAmount = this.Product.VIPPrice.Value * this.Quantity;
            }
        }
    }
}
