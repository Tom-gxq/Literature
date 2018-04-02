using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditAssociatorCommandHandler : ICommandHandler<EditAssociatorCommand>
    { 
        private IDataRepository<AssociatorDomain> _repository;

        public EditAssociatorCommandHandler(IDataRepository<AssociatorDomain> repository)
        {
            this._repository = repository;
        }

        public void Execute(EditAssociatorCommand command)
        {
            var aggregate = new AssociatorDomain();
            aggregate.EditAssociatorDomain(command.Id, command.Status);
            _repository.Save(aggregate);
        }

    }
}
