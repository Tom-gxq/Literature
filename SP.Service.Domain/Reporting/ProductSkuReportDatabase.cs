using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using StockGRPCInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class ProductSkuReportDatabase : IReportDatabase
    {
        private readonly ProductSkuRepository _repository;
        public ProductSkuReportDatabase(ProductSkuRepository repository)
        {
            _repository = repository;
        }
        public ProductSkuDomain GetProductSkuByProductId(int shopId,string productId, string host)
        {
            //var sku = _repository.GetProductSkuByProductId(shopId,productId, accountId);
            //return ConvertSkuEntityToDomain(sku);
            var response = StockBusiness.GetProductSku(host,productId, shopId);
            ProductSkuDomain domain = new ProductSkuDomain();
            domain.ShopId = shopId;
            domain.ProductId = productId;
            if (response != null && response.Sku != null && response.Sku.Count > 0)
            {
                domain.Stock = response.Sku[0].Stock;
            }
            return domain;
        }
        public ProductSkuDomain GetProductSku(int shopId, string productId, string accountId)
        {
            var sku = _repository.GetProductSkuByProductId(shopId, productId, accountId);
            return ConvertSkuEntityToDomain(sku);

        }
        public ProductSkuDomain GetPreDayProductSku(int shopId, string productId, string accountId)
        {
            var sku = _repository.GetPreDayProductSku(shopId, productId, accountId);
            return ConvertSkuEntityToDomain(sku);

        }

        public bool UpdateProductSkuStock(ProductSkuEntity entity)
        {
            var result = _repository.UpdateProductSkuStock(entity);
            return result > 0;
        }
        public bool RedoProductSkuStock(ProductSkuEntity entity)
        {
            var result = _repository.RedoProductSkuStock(entity);
            return result > 0;
        }
        public bool UpdateProductSkuOrderNum(ProductSkuEntity entity)
        {
            var result = _repository.UpdateProductSkuOrderNum(entity);
            return result > 0;
        }
        public bool AddProductSku(ProductSkuEntity entity)
        {
            var result = _repository.AddProductSku(entity);
            return result > 0;
        }
        public List<ProductSkuDomain> GetCurrentProductSku(int shopType)
        {
            List<ProductSkuDomain> list = new List<ProductSkuDomain>();
            var skuList = _repository.GetCurrentProductSku(shopType);
            foreach(var item in skuList)
            {                
                list.Add(ConvertSkuEntityToDomain(item));
            }
            return list;

        }
        private ProductSkuDomain ConvertSkuEntityToDomain(ProductSkuEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            var sku = new ProductSkuDomain();
            sku.SetMemento(entity);
            
            return sku;
        }
    }
}
