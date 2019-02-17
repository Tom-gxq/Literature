using Grpc.Service.Core.AutoMapper;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class SuppliersProductCommandHandler : ICommandHandler<CreateSuppliersProductCommand>, ICommandHandler<CreateSuppliersRegionCommand>
    {
        private IDataRepository<SuppliersProductDomain> _repository;
        public SuppliersProductCommandHandler(IDataRepository<SuppliersProductDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(CreateSuppliersProductCommand command)
        {
            var aggregate = command.ToDomain<SuppliersProductDomain>();
            aggregate.Create();
            _repository.Save(aggregate);
        }

        public void Execute(CreateSuppliersRegionCommand command)
        {
            var aggregate = command.ToDomain<SuppliersProductDomain>();
            aggregate.CreateRegion();
            _repository.Save(aggregate);
        }
    }
}
