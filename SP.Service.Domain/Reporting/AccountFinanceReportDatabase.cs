using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AccountFinanceReportDatabase: IReportDatabase
    {
        private readonly AccountFinanceRepository _repository;
        public AccountFinanceReportDatabase(AccountFinanceRepository repository)
        {
            _repository = repository;
        }

        public bool Add(AccountFinanceEntity item)
        {
            return _repository.Add(item);
        }

        public bool UpdateAccountFinance(AccountFinanceEntity account)
        {
            return _repository.UpdateAccountFinance(account);
        }
        public AccountFinanceDomain GetAccountFinanceDetail(string accountId)
        {
            var entity = _repository.GetAccountFinanceDetail(accountId);
            return ConvertEntityToDomain(entity);
        }
        private AccountFinanceDomain ConvertEntityToDomain(AccountFinanceEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            var order = new AccountFinanceDomain();
            order.SetMemento(entity);
            return order;
        }
    }
}
