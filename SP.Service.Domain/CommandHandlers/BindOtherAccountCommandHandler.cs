using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class BindOtherAccountCommandHandler : ICommandHandler<BindOtherAccountCommand>
    {
        private IDataRepository<AccountDomain> _repository;

        public BindOtherAccountCommandHandler(IDataRepository<AccountDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(BindOtherAccountCommand command)
        {
            var aggregate = new AccountDomain();
            aggregate.BindAccount(command.Id, command.OtherAccount,command.OtherType);
            _repository.Save(aggregate);
            
        }
    }
}
