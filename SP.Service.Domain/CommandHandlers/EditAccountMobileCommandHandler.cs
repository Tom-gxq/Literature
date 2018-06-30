using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditAccountMobileCommandHandler : ICommandHandler<EditAccountMobileCommand>
    {
        private IDataRepository<AccountDomain> _repository;

        public EditAccountMobileCommandHandler(IDataRepository<AccountDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditAccountMobileCommand command)
        {
            var aggregate = new AccountDomain();
            aggregate.EditAccountMobile(command.Id, command.Mobile);
            _repository.Save(aggregate);
        }
    }
}
