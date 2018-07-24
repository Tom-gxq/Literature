using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateApplyPartnerCommandHandler : ICommandHandler<CreateApplyPartnerCommand>
    {
        private IDataRepository<ApplyPartnerDomain> _repository;
        public CreateApplyPartnerCommandHandler(IDataRepository<ApplyPartnerDomain> repository)
        {
            this._repository = repository;
        }


        public void Execute(CreateApplyPartnerCommand command)
        {
            var aggregate = new ApplyPartnerDomain(command.Id, command.DormId);
            _repository.Save(aggregate);
        }
    }
}
