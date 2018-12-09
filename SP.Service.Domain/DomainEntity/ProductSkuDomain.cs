using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using SP.Producer;
using SP.Service.Domain.Events;
using SP.Service.Domain.Util;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class ProductSkuDomain : AggregateRoot<Guid>, IHandle<DecreaseProductSkuEvent>, 
        IHandle<RedoProductSkuEvent>, IHandle<ProductSkuDBUpdateEvent>, IHandle<KafkaAddEvent>,
        IHandle<ResidueSkuUpdateEvent> ,IOriginator
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
        public ProductSkuDomain(Guid id, string accountId, string productId, int shopId, int stock)
        {
            ApplyChange(new ProductSkuDBCreateEvent(id, accountId, productId, shopId, stock));
        }
        public void EditProductSkuOrderNum(Guid id,int shopId, string productId, string accountId, int orderNum)
        {
            ApplyChange(new ProductSkuOrderNumEvent(id,shopId, productId, accountId, orderNum));
        }
        public void EditProductSkuDomainStock(Guid id,int shopId, string productId, int stock, string orderId, string accountId)
        {
            string host = OrderCommon.GetHost();
            ApplyChange(new DecreaseProductSkuEvent(id,shopId, productId, stock, orderId, host, accountId));
        }
        public void RedoProductSkuDomainStock(Guid id,int shopId, string productId, int stock, string orderId, string accountId)
        {
            string host = OrderCommon.GetHost();
            ApplyChange(new RedoProductSkuEvent(id,shopId, productId, stock, orderId, host, accountId));
        }
        public void UpdateProductSkuDomainStock(Guid id,int shopId, string productId, string accountId, int stock,int type)
        {
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();
            string kafkaIP = string.Empty;
            if (config != null)
            {
                kafkaIP = config.GetSection("KafkaIP").Value?.ToString() ?? string.Empty;
            }
            var producer = new KafkaProductStockProducer();
            producer.IPConfig = kafkaIP;
            producer.AccountId = accountId;
            producer.ProductId = productId;
            producer.ShopId = shopId;
            producer.Stock = stock;
            producer.Type = type;
            ApplyChange(new KafkaAddEvent(id,producer));
        }
        public void UpdateProductSkuDbDomainStock(Guid id,int shopId, string productId, string accountId, int stock,int type)
        {
            ApplyChange(new ProductSkuDBUpdateEvent(id, accountId, productId, shopId,stock, type));
        }
        public void UpdateResidueSkuDomain(Guid id, int stock)
        {
            ApplyChange(new ResidueSkuUpdateEvent(id,  stock));
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
        public void Handle(ProductSkuDBUpdateEvent e)
        {
            this.ProductId = e.ProductId;
            this.ShopId = e.ShopId;
            this.AccountId = e.AccountId;
            this.Stock = e.Stock;
        }
        public void Handle(ProductSkuDBCreateEvent e)
        {
            this.ProductId = e.ProductId;
            this.ShopId = e.ShopId;
            this.AccountId = e.AccountId;
            this.Stock = e.Stock;
            this.SkuId = e.AggregateId.ToString();
        }
        public void Handle(KafkaAddEvent e)
        {

        }
        public void Handle(ResidueSkuUpdateEvent e)
        {

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
                this.AccountId = !string.IsNullOrEmpty(product.AccountId) ? product.AccountId : string.Empty;
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
