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
        private IDataRepository<SuppliersProductDomain> _repository;
        public EditProductCommandHandler(IDataRepository<SuppliersProductDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditProductCommand command)
        {
            var aggregate = new SuppliersProductDomain();
            aggregate.EditProduct(command.Id, command.ProductId, command.PurchasePrice, command.SuppliersId);
            _repository.Save(aggregate);
        }

        public void Execute(EditSaleStatusCommand command)
        {
            var aggregate = new SuppliersProductDomain();
            aggregate.EditProductSaleStatus(command.Id, command.Status, command.SuppliersId, command.ProductId);
            _repository.Save(aggregate);
        }
    }
}
