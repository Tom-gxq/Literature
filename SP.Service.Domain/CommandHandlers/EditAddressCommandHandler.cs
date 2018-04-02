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
    public class EditAddressCommandHandler : ICommandHandler<EditAddressCommand>
    {
        private IDataRepository<AccountAddressDomain> _repository;
        private AddressReportDatabase _accountReportDatabase;

        public EditAddressCommandHandler(IDataRepository<AccountAddressDomain> repository, AddressReportDatabase accountReportDatabase)
        {
            this._repository = repository;
            _accountReportDatabase = accountReportDatabase;
        }

        public void Execute(EditAddressCommand command)
        {
            var aggregate = new AccountAddressDomain();
            aggregate.EditAddressDomain(command.Id, command.AddressId, command.UserName, command.Gender, command.Mobile, command.RegionID, command.Address, command.AccountId,command.Dorm,command.IsDefault);
            _repository.Save(aggregate);
        }
    }
}
