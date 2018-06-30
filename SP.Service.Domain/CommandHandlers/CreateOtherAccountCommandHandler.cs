using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateOtherAccountCommandHandler : ICommandHandler<CreateOtherAccountCommand>
    {
        private IDataRepository<AccountDomain> _repository;

        public CreateOtherAccountCommandHandler(IDataRepository<AccountDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(CreateOtherAccountCommand command)
        {
            var aggregate = new AccountDomain(command.Id, command.MobilePhone, command.OtherAccount, command.OtherType, command.FullName, command.Avatar, command.Gender);
            _repository.Save(aggregate);

        }
    }
}
