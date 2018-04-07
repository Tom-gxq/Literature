using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
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
        public ProductSkuDomain GetProductSkuByProductId(int shopId,string productId)
        {
            var sku = _repository.GetProductSkuByProductId(shopId,productId);

            return ConvertSkuEntityToDomain(sku); ;
        }
        public bool UpdateProductSkuStock(ProductSkuEntity entity)
        {
            var result = _repository.UpdateProductSkuStock(entity);
            return result > 0;
        }
        public bool UpdateProductSkuOrderNum(ProductSkuEntity entity)
        {
            var result = _repository.UpdateProductSkuOrderNum(entity);
            return result > 0;
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
