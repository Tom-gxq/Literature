using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditAccountPwdCommandHandler : ICommandHandler<EditAccountPwdCommand>
    {
        private IDataRepository<AccountDomain> _repository;

        public EditAccountPwdCommandHandler(IDataRepository<AccountDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditAccountPwdCommand command)
        {
            var aggregate = new AccountDomain();
            aggregate.EditAccountPwd(command.Id, command.Pwd);
            _repository.Save(aggregate);
        }
    }
}
