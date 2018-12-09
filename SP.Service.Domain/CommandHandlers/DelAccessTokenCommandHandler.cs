using Grpc.Service.Core.AutoMapper;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class DelAccessTokenCommandHandler : ICommandHandler<DelAccessTokenCommand>
    {
        private IDataRepository<AccessTokenDomain> _repository;
        private AccessTokenReportDatabase _accessTokenReportDatabase;

        public DelAccessTokenCommandHandler(AccessTokenReportDatabase accessTokenReportDatabase)
        {
            _accessTokenReportDatabase = accessTokenReportDatabase;
        }

        public void Execute(DelAccessTokenCommand command)
        {
            var aggregate = command.ToDomain<AccessTokenDomain>();
            aggregate.Remove();
            _repository.Save(aggregate);
            //_accessTokenReportDatabase.RemoveAccessToken(command.AccessToken, command.AccountId);
        }
    }
}
