using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class DelProductCommandHandler : ICommandHandler<DelProductCommand>
    {
        private IDataRepository<ProductDomain> _repository;
        public DelProductCommandHandler(IDataRepository<ProductDomain> repository)
        {
            _repository = repository;
        }

        public void Execute(DelProductCommand command)
        {
            var aggregate = new ProductDomain();
            aggregate.DelProduct(command.Id);
            _repository.Save(aggregate);
        }
    }
}
