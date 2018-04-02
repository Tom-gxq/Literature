using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AuthenticationReportDatabase : IReportDatabase
    {
        private readonly AuthenticationRepository _repository;
        public AuthenticationReportDatabase(AuthenticationRepository repository)
        {
            _repository = repository;
        }

        public bool Add(AuthenticationEntity item)
        {
            return _repository.Add(item);
        }

        public bool UpdateAuthenticationStatus(AuthenticationEntity item)
        {
            return _repository.UpdateAuthenticationStatus(item);
        }

        public string GetVerifyRegisterCode(int authType, string account, string accountId)
        {
            return _repository.GetValidVerifyCodeByAccount(account, authType, accountId);
        }
    }
}
