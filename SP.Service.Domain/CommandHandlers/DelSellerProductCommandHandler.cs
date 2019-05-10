﻿using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class DelSellerProductCommandHandler : ICommandHandler<DelSellerProductCommand>
    {
        private IDataRepository<SellerProductDomain> _repository;
        public DelSellerProductCommandHandler(IDataRepository<SellerProductDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(DelSellerProductCommand command)
        {
            var aggregate = new SellerProductDomain();
            aggregate.DelProduct(command.Id, command.AccountId, command.SupplierProductId);
            _repository.Save(aggregate);
        }
    }
}