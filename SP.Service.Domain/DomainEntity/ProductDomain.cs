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
    public class ProductDomain:AggregateRoot<Guid>, IHandle<ProductCreatedEvent>, 
        IHandle<ProductImageCreatedEvent>, IHandle<SaleStatusEditEvent>, IOriginator
    {
        public string ProductId { get; internal set; }
        public string ProductName { get; internal set; }
        public string ProductCode { get; internal set; }
        public string ShortDescription { get; internal set; }
        public string Unit { get; internal set; }
        public string Description { get; internal set; }
        public int SaleStatus { get; internal set; }
        public int SkuNum { get; internal set; }
        public int ShopId { get; internal set; }
        public double Price { get; internal set; }
        public string SkuId { get; internal set; }
        public DateTime AddedDate { get; internal set; }
        public double MarketPrice { get; internal set; }
        public double VipPrice { get; internal set; }
        public BrandEntity Brand { get; internal set; }
        public ProductTypeEntity ProductType { get; internal set; }
        public DateTime UpdateTime { get; internal set; }
        public List<ProductImageEntity> Images { get; internal set; }
        public List<ProductAttributeInfoEntity> Attributes { get; internal set; }
        public long TypeId { get; internal set; }
        public long SecondTypeId { get; internal set; }
        public string SuppliersId { get; internal set; }
        public double PurchasePrice { get; internal set; }
        public string ImagePath { get; internal set; }

        public ProductDomain()
        {

        }
        public ProductDomain(Guid id,long mainType,long secondType, string productName,string suppliersId, double marketPrice,double purchasePrice,string imagePath)
        {
            ApplyChange(new ProductCreatedEvent(id, mainType, secondType, productName, suppliersId, marketPrice, purchasePrice));
            ApplyChange(new ProductImageCreatedEvent(id,imagePath));
        }
        public void EditProduct(Guid id, string productName, double marketPrice, double purchasePrice, string imagePath)
        {
            ApplyChange(new ProductEditEvent(id, productName, marketPrice, purchasePrice));
            ApplyChange(new ProductImageEditEvent(id, imagePath));
        }

        public void EditProductSaleStatus(Guid id, int status)
        {
            ApplyChange(new SaleStatusEditEvent(id, status));
        }
        public void Handle(ProductCreatedEvent e)
        {
            this.Id = e.AggregateId;
            this.TypeId = e.MainType;
            this.SecondTypeId = e.SecondType;
            this.ProductName = e.ProductName;
            this.SuppliersId = e.SuppliersId;
            this.MarketPrice = e.MarketPrice;
            this.PurchasePrice = e.PurchasePrice;
            
        }
        public void Handle(ProductImageCreatedEvent e)
        {
            this.ImagePath = e.ImagePath;
        }
        public void Handle(ProductEditEvent e)
        {
            this.Id = e.AggregateId;
            this.ProductName = e.ProductName;
            this.MarketPrice = e.MarketPrice;
            this.PurchasePrice = e.PurchasePrice;
        }
        public void Handle(SaleStatusEditEvent e)
        {
            this.SaleStatus = e.Status;
        }
        public void SetMemento(BaseEntity memento)
        {
            if(memento == null)
            {
                return;
            }
            var product = memento as ProductEntity;
            this.ProductId = product.ProductId;
            this.ProductName = product.ProductName;
            this.ProductCode = product.ProductCode;
            this.MarketPrice = product.MarketPrice != null? product.MarketPrice.Value:0;
            this.VipPrice = product.VIPPrice != null ? product.VIPPrice.Value : 0;
            this.ShortDescription = product.ShortDescription;
            this.Description = product.Description;
            this.Unit = product.Unit;            
            this.SaleStatus = product.SaleStatus != null? product.SaleStatus.Value:0;
            this.AddedDate = product.AddedDate != null ? product.AddedDate.Value : DateTime.MinValue;
            this.PurchasePrice = product.PurchasePrice != null ? product.PurchasePrice.Value : 0;
            this.SecondTypeId = product.SecondTypeId != null ? product.SecondTypeId.Value : 0;
            this.TypeId = product.TypeId != null ? product.TypeId.Value : 0;

            if (memento is ProductFullEntity)
            {
                var productFull = memento as ProductFullEntity;
                this.SkuNum = productFull.Stock != null ? productFull.Stock.Value : 0;
                this.SkuId = !string.IsNullOrEmpty(productFull.SkuId) ? productFull.SkuId : string.Empty;
                this.Price = productFull.Price != null ? productFull.Price.Value : 0;
                this.ShopId = productFull.ShopId != null ? productFull.ShopId.Value : 0;
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
