using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AccountInfoReportDatabase : IReportDatabase
    {
        private readonly AccountInfoRepository _repository;
        public AccountInfoReportDatabase(AccountInfoRepository repository)
        {
            _repository = repository;
        }
        public AccountInfoDomain GetAccountInfoById(string accountId)
        {
            var  entity =_repository.GetAccountInfoById(accountId);
            return ConvertEntityToDomain(entity);
        }
        public bool UpdateAccountFullInfo(AccountInfoEntity entity)
        {
            var result = _repository.UpdateAccountFullInfo(entity);
            return result;
        }

        private AccountInfoDomain ConvertEntityToDomain(AccountInfoEntity entity)
        {
            var account = new AccountInfoDomain();
            account.SetMemento(entity);
            return account;
        }
    }
}
