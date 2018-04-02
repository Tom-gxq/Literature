using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditAuthenticationCommandHandler : ICommandHandler<EditAuthenticationCommand>
    {
        private IDataRepository<AuthenticationDomain> _repository;

        public EditAuthenticationCommandHandler(IDataRepository<AuthenticationDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditAuthenticationCommand command)
        {
            var aggregate = new AuthenticationDomain();
            aggregate.EditAuthenticationDomain(command.Id, command.AuthType, command.AccountId, command.Account, command.Status);
            _repository.Save(aggregate);
        }
    }
}
