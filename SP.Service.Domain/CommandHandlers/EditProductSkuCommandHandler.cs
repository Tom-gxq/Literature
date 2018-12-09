using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditProductSkuCommandHandler : ICommandHandler<EditProductSkuCommand>,
        ICommandHandler<EditProductSkuDBCommand>, ICommandHandler<CreateProductSkuDBCommand>
    {
        private IDataRepository<ProductSkuDomain> _repository;
        public EditProductSkuCommandHandler(IDataRepository<ProductSkuDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditProductSkuCommand command)
        {
            var aggregate = new ProductSkuDomain();
            aggregate.UpdateProductSkuDomainStock(command.Id,command.ShopId, command.ProductId, command.AccountId, command.Stock, command.Type);
            _repository.Save(aggregate);
        }

        public void Execute(EditProductSkuDBCommand command)
        {
            var aggregate = new ProductSkuDomain();
            aggregate.UpdateProductSkuDbDomainStock(command.Id,command.ShopId, command.ProductId, command.AccountId, command.Stock,command.Type);
            _repository.Save(aggregate);
        }

        public void Execute(CreateProductSkuDBCommand command)
        {
            var aggregate = new ProductSkuDomain(command.Id, command.AccountId, command.ProductId, command.ShopId, command.Stock);
            _repository.Save(aggregate);
        }
    }
}
