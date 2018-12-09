using Grpc.Service.Core.AutoMapper;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.BalancePay;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class BalancePayCommandHandler : ICommandHandler<BalancePayCommand>
    {
        private IDataRepository<BalancePayDomain> _repository;
        public BalancePayCommandHandler(IDataRepository<BalancePayDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(BalancePayCommand command)
        {
            var aggregate = command.ToDomain<BalancePayDomain>();
            aggregate.Create();
            _repository.Save(aggregate);
        }
    }
}
