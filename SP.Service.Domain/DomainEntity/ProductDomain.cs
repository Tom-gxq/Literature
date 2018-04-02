using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class ProductDomain:AggregateRoot<Guid>, IOriginator
    {
        public string ProductId { get; internal set; }
        public string ProductName { get; internal set; }
        public string ProductCode { get; internal set; }
        public string ShortDescription { get; internal set; }
        public string Unit { get; internal set; }
        public string Description { get; internal set; }
        public int SaleStatus { get; internal set; }
        public int SkuNum { get; internal set; }
        public double Price { get; set; }
        public string SkuId { get; set; }
        public DateTime AddedDate { get; internal set; }
        public double MarketPrice { get; internal set; }
        public double VipPrice { get; internal set; }
        public BrandEntity Brand { get; internal set; }
        public ProductTypeEntity ProductType { get; internal set; }
        public DateTime UpdateTime { get; internal set; }
        public List<ProductImageEntity> Images { get; internal set; }
        public List<ProductAttributeInfoEntity> Attributes { get; internal set; }

        public void SetMemento(BaseEntity memento)
        {
            var product = memento as ProductEntity;
            this.ProductId = product.ProductId;
            this.ProductName = product.ProductName;
            this.ProductCode = product.ProductCode;
            this.MarketPrice = product.MarketPrice.Value;
            this.VipPrice = product.VIPPrice != null ? product.VIPPrice.Value : 0;
            this.ShortDescription = product.ShortDescription;
            this.Description = product.Description;
            this.Unit = product.Unit;            
            this.SaleStatus = product.SaleStatus.Value;
            this.AddedDate = product.AddedDate != null ? product.AddedDate.Value : DateTime.MinValue;
            if (memento is ProductFullEntity)
            {
                var productFull = memento as ProductFullEntity;
                this.SkuNum = productFull.Stock != null ? productFull.Stock.Value : 0;
                this.SkuId = productFull.SkuId;
                this.Price = productFull.Price != null ? productFull.Price.Value : 0;
            }
        }
        public void SetMemenBrandto(BrandEntity memento)
        {
            if (memento is BrandEntity)
            {
                this.Brand = memento;
            }
        }

        public void SetMemenTypeto(ProductTypeEntity memento)
        {
            if (memento is ProductTypeEntity)
            {
                this.ProductType = memento;
            }
        }

        public void SetMemenImagesto(List<ProductImageEntity> memento)
        {
            if (memento != null)
            {
                this.Images = memento;
            }
        }

        public void SetMemenAttributeto(List<ProductAttributeInfoEntity> memento)
        {
            if (memento != null)
            {
                this.Attributes = memento;
            }
        }

        public BaseEntity GetMemento()
        {
            return new ProductEntity()
            {
               ProductId = this.ProductId,
               ProductName = this.ProductName,
               ProductCode = this.ProductCode,
               MarketPrice = this.MarketPrice,
               VIPPrice = this.VipPrice,
               ShortDescription = this.ShortDescription,
               Description = this.Description,
               Unit = this.Unit,
               SaleStatus =this.SaleStatus,
               AddedDate =this.AddedDate
            };
        }
    }
}
