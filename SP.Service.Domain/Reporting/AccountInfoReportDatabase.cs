using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AccountInfoReportDatabase : IReportDatabase
    {
        private readonly AccountInfoRepository _repository;
        private readonly AssociatorRepository _associatorRepository;
        public AccountInfoReportDatabase(AccountInfoRepository repository, AssociatorRepository associatorRepository)
        {
            _repository = repository;
            _associatorRepository = associatorRepository;
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

        public DateTime GetAssociatorDateByAId(string accountId)
        {
            var list = _associatorRepository.GetMemberByAccountId(accountId);
            if (list != null)
            {
                var endDate = list.Max(x => x.EndDate);
                if(endDate != null)
                {
                    return endDate.Value;
                }
            }
            return DateTime.MinValue;
        }
        
        private AccountInfoDomain ConvertEntityToDomain(AccountInfoEntity entity)
        {
            var account = new AccountInfoDomain();
            account.SetMemento(entity);
            return account;
        }
    }
}
