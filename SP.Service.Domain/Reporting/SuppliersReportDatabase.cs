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
        private readonly SupplersRepository _repository;
        public SuppliersReportDatabase(SuppliersProductRepository productRepository, SuppliersRegionRespository regionRepository, SupplersRepository repository)
        {
            _productRepository = productRepository;
            _regionRepository = regionRepository;
            _repository = repository;
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

        public List<SuppliersProductFullEntity> GetSuppliersProduct(int mainType, int secondType, int supplierId)
        {
            return _regionRepository.GetSuppliersProduct( mainType,  secondType,  supplierId);
        }

        public SuppliersEntity GetSupplierInfo(string accountId)
        {
            return _repository.GetSupplierInfo(accountId);
        }

        public List<SuppliersProductFullEntity> GetSellerProductList(int regionId,int typeId, int pageIndex, int pageSize)
        {
            return _productRepository.GetSellerProductList(regionId, typeId, pageIndex, pageSize);
        }

        public List<SuppliersProductFullEntity> GetSellerProductListByAccountId(string accounId, int pageIndex, int pageSize)
        {
            return _productRepository.GetSellerProductListByAccountId(accounId, pageIndex, pageSize);
        }

        public SuppliersProductFullEntity GetSuppliersProductById(int supplierProductId)
        {
            var list =  _productRepository.GetSuppliersProductById(supplierProductId);
            if(list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }

        }
    }
}
