using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.StockShip;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreatShipOrderCommandHandler : ICommandHandler<CreatShipOrderCommand>
    {
        private IDataRepository<ShipOrderDomain> _repository;

        public CreatShipOrderCommandHandler(IDataRepository<ShipOrderDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(CreatShipOrderCommand command)
        {
            var aggregate = new ShipOrderDomain(command.OrderId, command.ShippingId,command.ShipTo, command.ShippingDate, command.Stock, command.ProductId, command.ShopId);
            
            _repository.Save(aggregate);
        }
    }
}
