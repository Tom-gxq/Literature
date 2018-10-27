using AutoMapper;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Token;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.DomainFactory;
using Grpc.Service.Core.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class TokenCommandHandler: ICommandHandler<GenerateCommand>, 
        ICommandHandler<UpdateStatusCommand>
    {
        private IDataRepository<RepeatedTokenDomain> _repository;

        public TokenCommandHandler(IDataRepository<RepeatedTokenDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(GenerateCommand command)
        {
            var aggregate = command.ToDomain<RepeatedTokenDomain>();
            aggregate.Create();
            _repository.Save(aggregate);
        }

        public void Execute(UpdateStatusCommand command)
        {
            var aggregate = command.ToDomain<RepeatedTokenDomain>();
            aggregate.Update();
            _repository.Save(aggregate);
        }
    }
}
