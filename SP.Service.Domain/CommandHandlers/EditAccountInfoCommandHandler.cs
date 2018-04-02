using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditAccountInfoCommandHandler : ICommandHandler<EditAccountInfoCommand>
    {
        private IDataRepository<AccountInfoDomain> _repository;
        public EditAccountInfoCommandHandler(IDataRepository<AccountInfoDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditAccountInfoCommand command)
        {
            var aggregate = new AccountInfoDomain();
            aggregate.EditAccountInfoDomain(command.AccountId, command.FullName, command.Gender, command.Avatar);
            _repository.Save(aggregate);
        }
    }
}
