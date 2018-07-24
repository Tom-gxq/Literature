using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class ProductImageReportDatabase : IReportDatabase
    {
        private readonly ProductImageRepository _repository;

        public ProductImageReportDatabase(ProductImageRepository repository)
        {
            _repository = repository;
        }

        public bool Add(ProductImageEntity item)
        {
            return _repository.Add(item);
        }
        public bool Update(ProductImageEntity item)
        {
            var ret= _repository.Update(item);
            return ret > 0;
        }
        public ProductImageEntity GetProductImage(string prductId)
        {
            var productEntity = _repository.GetProductImageById(prductId);
            return productEntity;
        }
    }
}
