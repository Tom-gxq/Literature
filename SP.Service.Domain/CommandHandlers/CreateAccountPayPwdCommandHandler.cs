using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateAccountPayPwdCommandHandler : ICommandHandler<CreateAccountPayPwdCommand>
    {
        private IDataRepository<AccountInfoDomain> _repository;

        public CreateAccountPayPwdCommandHandler(IDataRepository<AccountInfoDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(CreateAccountPayPwdCommand command)
        {           
            var aggregate = new AccountInfoDomain();
            aggregate.SetAccountPayPwd(command.Id.ToString(), command.PayPwd);
            _repository.Save(aggregate);
        }
    }
}
