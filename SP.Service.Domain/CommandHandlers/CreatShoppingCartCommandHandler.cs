using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.ShoppingCart;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreatShoppingCartCommandHandler : ICommandHandler<CreatShoppingCartCommand>
    {
        private IDataRepository<ShoppingCartsDomain> _repository;

        public CreatShoppingCartCommandHandler(IDataRepository<ShoppingCartsDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(CreatShoppingCartCommand command)
        {
            var aggregate = new ShoppingCartsDomain(command.Id, command.AccountId, command.Quantity, command.ProductId,command.ShopId);

            _repository.Save(aggregate);
        }
    }
}
