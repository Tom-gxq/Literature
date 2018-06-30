﻿using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class ProductSkuDomain : AggregateRoot<Guid>, IHandle<DecreaseProductSkuEvent>, IHandle<RedoProductSkuEvent>, IOriginator
    {
        public string ProductId { get; internal set; }
        public string SkuId { get; internal set; }
        public string SKU { get; internal set; }
        public int Stock { get; internal set; }
        public DateTime EffectiveTime { get; internal set; }
        public int AlertStock { get; internal set; }
        public double Price { get; internal set; }
        public int ShopId { get; internal set; }
        public int OrderNum { get; internal set; }
        public string AccountId { get; internal set; }

        public ProductSkuDomain()
        {

        }
        public void EditProductSkuOrderNum(int shopId, string productId, int orderNum)
        {
            ApplyChange(new ProductSkuOrderNumEvent(shopId, productId, orderNum));
        }
        public void EditProductSkuDomainStock(int shopId,string productId, int stock)
        {
            ApplyChange(new DecreaseProductSkuEvent(shopId,productId, stock));
        }
        public void RedoProductSkuDomainStock(int shopId, string productId, int stock)
        {
            ApplyChange(new RedoProductSkuEvent(shopId,productId, stock));
        }
        public void Handle(DecreaseProductSkuEvent e)
        {
            this.ProductId = e.ProductId;
            this.Stock = e.DecStock;
            this.ShopId = e.ShopId;
        }
        public void Handle(RedoProductSkuEvent e)
        {
            this.ProductId = e.ProductId;
            this.Stock = e.RedoStock;
            this.ShopId = e.ShopId;
        }
        public void Handle(ProductSkuOrderNumEvent e)
        {
            this.ProductId = e.ProductId;
            this.OrderNum = e.OrderNum;
            this.ShopId = e.ShopId;
        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is ProductSkuEntity)
            {
                var product = memento as ProductSkuEntity;
                this.ProductId = product.ProductId;
                this.SkuId = product.SkuId;
                this.SKU = product.SKU;
                this.ShopId = product.ShopId != null ? product.ShopId.Value : 0;
                this.Stock = product.Stock!= null ? product.Stock.Value:0;
                this.AlertStock = product.AlertStock != null ? product.AlertStock.Value : 0;
                this.Price = product.Price != null ? product.Price.Value : 0;
                this.EffectiveTime = product.EffectiveTime != null ? product.EffectiveTime.Value : DateTime.MinValue;
                this.AccountId = string.IsNullOrEmpty(product.AccountId) ? product.AccountId : string.Empty;
                this.OrderNum = product.OrderNum != null? product.OrderNum.Value : 0;
            }
        }
        public BaseEntity GetMemento()
        {
            return new ProductSkuEntity()
            {
                ProductId = this.ProductId,
                SkuId = this.SkuId,
                SKU = this.SKU,
                Stock = this.Stock,
                AlertStock = this.AlertStock,
                Price = this.Price,
                ShopId = this.ShopId,
                EffectiveTime =this.EffectiveTime,
                AccountId = this.AccountId,
                OrderNum = this.OrderNum
            };
        }
    }
}