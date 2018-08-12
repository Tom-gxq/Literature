using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand>
    {
        private IDataRepository<ProductDomain> _repository;
        public CreateProductCommandHandler(IDataRepository<ProductDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(CreateProductCommand command)
        {            
            var aggregate = new ProductDomain(command.Id, command.MainType, command.SecondType, command.ProductName, command.SuppliersId, command.MarketPrice, command.PurchasePrice, command.ImagePath, command.VIPPrice);
            
            _repository.Save(aggregate);
        }
    }
}
