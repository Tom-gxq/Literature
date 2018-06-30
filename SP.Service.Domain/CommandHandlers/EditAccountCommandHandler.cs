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
    public class EditAccountCommandHandler : ICommandHandler<EditAccountCommand>
    {
        private IDataRepository<AccountDomain> _repository;

        public EditAccountCommandHandler(IDataRepository<AccountDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditAccountCommand command)
        {
            var aggregate = new AccountDomain();
            aggregate.EditAccount(command.Id, command.MobilePhone, command.Email, command.Password);
            _repository.Save(aggregate);            
        }
    }
}
