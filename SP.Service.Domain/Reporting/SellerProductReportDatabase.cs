using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class SellerProductReportDatabase : IReportDatabase
    {
        private readonly SellerProductRepository _repository;
        public SellerProductReportDatabase(SellerProductRepository repository)
        {
            this._repository = repository;
        }

        public bool Add(SellerProductEntity item)
        {
            return _repository.Add(item);
        }
        public bool Del(SellerProductEntity item)
        {
            return _repository.Delete(item);
        }

        public SellerProductEntity GetSellerProduct(string accountId,int supplierProductId)
        {
            return _repository.GetSellerProduct(accountId, supplierProductId);
        }
    }
}
