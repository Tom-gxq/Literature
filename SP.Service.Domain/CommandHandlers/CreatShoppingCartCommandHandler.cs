using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.ShoppingCart;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreatShoppingCartCommandHandler : ICommandHandler<CreatShoppingCartCommand>
    {
        private IDataRepository<ShoppingCartsDomain> _repository;
        private readonly ShoppingCartReportDatabase _reportDatabase;
        private readonly ShopReportDatabase _shopReportDatabase;
        private static object lockObj = new object();

        public CreatShoppingCartCommandHandler(IDataRepository<ShoppingCartsDomain> repository, ShoppingCartReportDatabase reportDatabase,
            ShopReportDatabase shopReportDatabase)
        {
            this._repository = repository;
            this._reportDatabase = reportDatabase;
            this._shopReportDatabase = shopReportDatabase;
        }

        public void Execute(CreatShoppingCartCommand command)
        {
            lock (lockObj)
            {
                ShoppingCartsDomain aggregate = null;
                var domain = _reportDatabase.GetShoppingCart(command.AccountId, command.ShopId, command.ProductId);
                if (domain == null)
                {
                    var shop = _shopReportDatabase.GetShopById(command.ShopId);
                    aggregate = new ShoppingCartsDomain(command.Id, command.AccountId, command.Quantity, command.ProductId, command.ShopId, shop?.OwnerId??string.Empty);
                }
                else
                {
                    var quantity = domain.Quantity + command.Quantity;
                    aggregate = new ShoppingCartsDomain();
                    aggregate.AddShoppingCartNum(command.Id, command.AccountId, quantity, command.ProductId, command.ShopId);
                }
                _repository.Save(aggregate);
            }
        }
    }
}
