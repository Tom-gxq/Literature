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
    public class CreateAccessTokenCommandHandler : ICommandHandler<CreateAccessTokenCommand>
    {
        private IDataRepository<AccessTokenDomain> _repository;
        private AccessTokenReportDatabase _accessTokenReportDatabase;

        public CreateAccessTokenCommandHandler(IDataRepository<AccessTokenDomain> repository, AccessTokenReportDatabase accessTokenReportDatabase)
        {
            this._repository = repository;
            _accessTokenReportDatabase = accessTokenReportDatabase;
        }

        public void Execute(CreateAccessTokenCommand command)
        {
            var aggregate = new AccessTokenDomain( command.AccountId, command.AccessToken, command.AccessTokenExpires, command.RefreshToken, command.RefreshTokenExpires,command.CreateTime);

            _repository.Save(aggregate);
        }
    }
}
