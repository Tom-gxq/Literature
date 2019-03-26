using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class ProductTypeReportDatabase : IReportDatabase
    {
        private readonly ProductTypeRepository _repository;
        public ProductTypeReportDatabase(ProductTypeRepository repository)
        {
            _repository = repository;
        }

        public List<ProductTypeDomain> GetProductTypeByKind(int kind)
        {
            var retList = new List<ProductTypeDomain>();
            var list = _repository.GetProductTypeByKind(kind);
            
            foreach (var item in list)
            {
                var domain = new ProductTypeDomain();
                domain.SetMemento(item);
                retList.Add(domain);
            }
            return retList;
        }

        public ProductTypeDomain GetSuppliersType(int supplierId)
        {
            var entity = _repository.GetProductTypeById(supplierId);

            var domain = new ProductTypeDomain();
            domain.SetMemento(entity);

            return domain;
        }
    }
}
