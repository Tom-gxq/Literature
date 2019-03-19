using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class SuppliersReportDatabase : IReportDatabase
    {
        private readonly SuppliersProductRepository _productRepository;
        private readonly SuppliersRegionRespository _regionRepository;
        public SuppliersReportDatabase(SuppliersProductRepository productRepository, SuppliersRegionRespository regionRepository)
        {
            _productRepository = productRepository;
            _regionRepository = regionRepository;
        }

        public void AddSuppliersProduct(SuppliersProductEntity item)
        {
            _productRepository.AddSuppliers(item);
        }
        public void AddSuppliersRegion(SuppliersRegionEntity item)
        {
            _regionRepository.AddSuppliersRegion(item);
        }

        public List<SuppliersProductFullEntity> GetSuppliersProduct(int supplierId)
        {
            return _regionRepository.GetSuppliersProduct(supplierId);
        }
    }
}
