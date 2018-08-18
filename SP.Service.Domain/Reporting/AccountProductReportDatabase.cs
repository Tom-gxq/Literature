using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AccountProductReportDatabase : IReportDatabase
    {
        private readonly AccountProductRespository _repository;
        public AccountProductReportDatabase(AccountProductRespository repository)
        {
            _repository = repository;
        }

        public bool Add(AccountProductEntity item)
        {
            return _repository.AddAccountProduct(item);
        }
        public AccountProductEntity GetAccountProduct(string accountId, string productId, int shopId)
        {
            return _repository.GetAccountProduct(accountId, productId, shopId);
        }
    }
}
