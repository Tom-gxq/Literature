using Grpc.Service.Core.AutoMapper;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class AccountWxCommandHandler : ICommandHandler<EditWxUnionIdCommand>,ICommandHandler<CreateWxOpenIdCommand>, 
        ICommandHandler<CreateAccountWxUnionIdCommand>
    {
        private IDataRepository<AccountDomain> _repository;
        public AccountWxCommandHandler(IDataRepository<AccountDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditWxUnionIdCommand command)
        {
            var aggregate = command.ToDomain<AccountDomain>();
            aggregate.EditWxUnionId(command.Id,command.AccountId,command.WxUnionId);
            _repository.Save(aggregate);
        }
        public void Execute(CreateWxOpenIdCommand command)
        {
            var aggregate = command.ToDomain<AccountDomain>();
            aggregate.CreateOpenId(command.Id, command.AccountId, command.WxOpenId, command.WxType);
            _repository.Save(aggregate);
        }

        public void Execute(CreateAccountWxUnionIdCommand command)
        {
            var aggregate = command.ToDomain<AccountDomain>();
            aggregate.CreateAccountWxUnionId();
            _repository.Save(aggregate);
        }
    }
}
