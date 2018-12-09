using Grpc.Service.Core.AutoMapper;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateAddressCommandHandler : ICommandHandler<CreatAddressCommand>
    {
        private IDataRepository<AccountAddressDomain> _repository;
        private AddressReportDatabase _accountReportDatabase;

        public CreateAddressCommandHandler(IDataRepository<AccountAddressDomain> repository, AddressReportDatabase accountReportDatabase)
        {
            this._repository = repository;
            _accountReportDatabase = accountReportDatabase;
        }

        public void Execute(CreatAddressCommand command)
        {
            //var aggregate = new AccountAddressDomain(command.Id, command.UserName, command.Gender, command.Mobile, command.RegionID, command.Address, command.AccountId,command.Dorm,command.IsDefault);
            var aggregate = command.ToDomain<AccountAddressDomain>();
            aggregate.Create();
            _repository.Save(aggregate);
        }
    }
}
