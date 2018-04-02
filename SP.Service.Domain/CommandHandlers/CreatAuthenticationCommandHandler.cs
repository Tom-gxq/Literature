using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreatAuthenticationCommandHandler : ICommandHandler<CreatAuthenticationCommand>
    {
        private IDataRepository<AuthenticationDomain> _repository;

        public CreatAuthenticationCommandHandler(IDataRepository<AuthenticationDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(CreatAuthenticationCommand command)
        {
            var aggregate = new AuthenticationDomain(command.Id, command.AuthType, command.AccountId, command.Account, command.VerifyCode, command.Token);

            _repository.Save(aggregate);
        }
    }
}
