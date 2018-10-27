using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.DomainFactory;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class RepeatedTokenReportDatabase: BaseReportDatabase
    {
        private readonly RepeatedTokenRespository _repository;
        public RepeatedTokenReportDatabase(RepeatedTokenDomainFactory factory,RepeatedTokenRespository repository):base(factory)
        {
            _repository = repository;
        }

        public bool Add(RepeatedTokenEntity item)
        {
            return _repository.AddToken(item);
        }
        public bool Update(RepeatedTokenEntity item)
        {
            return _repository.UpdateToken(item);
        }
        public RepeatedTokenDomain GetTokenByKey(string accountId, string userKey)
        {
            var token = _repository.GetTokenByKey(accountId, userKey);

            var domain = this.domainFactory.GenerateDomain();
            domain.SetMemento(token);
            return domain as RepeatedTokenDomain;
        }
    }
}
