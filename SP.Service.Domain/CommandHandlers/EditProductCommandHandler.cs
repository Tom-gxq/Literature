using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditProductCommandHandler : ICommandHandler<EditProductCommand>, ICommandHandler<EditSaleStatusCommand>
    {
        private IDataRepository<ProductDomain> _repository;
        public EditProductCommandHandler(IDataRepository<ProductDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditProductCommand command)
        {
            var aggregate = new ProductDomain();
            aggregate.EditProduct(command.Id, command.ProductName, command.MarketPrice, command.PurchasePrice, command.ImagePath);
            _repository.Save(aggregate);
        }

        public void Execute(EditSaleStatusCommand command)
        {
            var aggregate = new ProductDomain();
            aggregate.EditProductSaleStatus(command.Id, command.Status);
            _repository.Save(aggregate);
        }
    }
}
