﻿using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Order;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateCashApplyCommandHandler:ICommandHandler<CreateCashApplyCommand>
    {
        private IDataRepository<CashApplyDomain> _repository;

        public CreateCashApplyCommandHandler(IDataRepository<CashApplyDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(CreateCashApplyCommand command)
        {
            var aggregate = new CashApplyDomain(command.AccountId, command.Alipay, command.Money);

            _repository.Save(aggregate);
        }
    }
}
