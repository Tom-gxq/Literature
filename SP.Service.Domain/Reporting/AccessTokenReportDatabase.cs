using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AccessTokenReportDatabase : IReportDatabase
    {
        private readonly AccessTokenRepository _repository;
        public AccessTokenReportDatabase(AccessTokenRepository repository)
        {
            _repository = repository;
        }

        public void Add(OAuth2AccessToken item)
        {
            _repository.AddAccessToken(item);
        }

        public AccessTokenDomain GetAccessTokenByKey(string userKey)
        {
            var account = _repository.GetAccessTokenByKey(userKey);

            return ConvertOrderEntityToDomain(account); ;
        }

        public bool RemoveAccessToken(string userKey,string accountId)
        {
            return _repository.RemoveAccessToken(userKey, accountId);
        }

        private AccessTokenDomain ConvertOrderEntityToDomain(OAuth2AccessToken entity)
        {
            var account = new AccessTokenDomain();
            account.SetMemento(entity);
            return account;
        }
    }
}
