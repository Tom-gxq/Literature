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
    public class ProductSkuDomain : AggregateRoot<Guid>, IHandle<DecreaseProductSkuEvent>, IHandle<RedoProductSkuEvent>, IOriginator
    {
        public string ProductId { get; internal set; }
        public string SkuId { get; internal set; }
        public string SKU { get; internal set; }
        public int Stock { get; internal set; }
        public DateTime EffectiveTime { get; internal set; }
        public int AlertStock { get; internal set; }
        public double Price { get; internal set; }

        public ProductSkuDomain()
        {

        }
        public void EditProductSkuDomainStock(string productId, int stock)
        {
            ApplyChange(new DecreaseProductSkuEvent(productId, stock));
        }
        public void RedoProductSkuDomainStock(string productId, int stock)
        {
            ApplyChange(new RedoProductSkuEvent(productId, stock));
        }
        public void Handle(DecreaseProductSkuEvent e)
        {
            this.ProductId = e.ProductId;
            this.Stock = e.DecStock;
        }
        public void Handle(RedoProductSkuEvent e)
        {
            this.ProductId = e.ProductId;
            this.Stock = e.RedoStock;
        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is ProductSkuEntity)
            {
                var product = memento as ProductSkuEntity;
                this.ProductId = product.ProductId;
                this.SkuId = product.SkuId;
                this.SKU = product.SKU;
                this.Stock = product.Stock!= null ? product.Stock.Value:0;
                this.AlertStock = product.AlertStock != null ? product.AlertStock.Value : 0;
                this.Price = product.Price != null ? product.Price.Value : 0; ;
                this.EffectiveTime = product.EffectiveTime != null ? product.EffectiveTime.Value : DateTime.MinValue;
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
                EffectiveTime =this.EffectiveTime
            };
        }
    }
}
