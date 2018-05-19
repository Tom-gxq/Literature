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
    public class EditAccountInfoCommandHandler : ICommandHandler<EditAccountInfoCommand>
    {
        private IDataRepository<AccountInfoDomain> _repository;
        private IDataRepository<AccountAddressDomain> _addressRepository;
        private AddressReportDatabase _accountAddressReportDatabase;
        private AccountReportDatabase _accountReportDatabase;
        private static object lockObj = new object();
        private static object lockSecondObj = new object();

        public EditAccountInfoCommandHandler(IDataRepository<AccountInfoDomain> repository, IDataRepository<AccountAddressDomain> addressRepository,
            AddressReportDatabase accountAddressReportDatabase,
            AccountReportDatabase accountReportDatabase)
        {
            this._repository = repository;
            this._addressRepository = addressRepository;
            this._accountAddressReportDatabase = accountAddressReportDatabase;
            this._accountReportDatabase = accountReportDatabase;
        }

        public void Execute(EditAccountInfoCommand command)
        {
            lock (lockObj)
            {
                lock (lockSecondObj)
                {
                    var aggregate = new AccountInfoDomain();
                    aggregate.EditAccountInfoDomain(command.AccountId, command.FullName, command.Gender, command.Avatar, command.UserType);
                    _repository.Save(aggregate);

                    var account = _accountReportDatabase.GetAccountById(command.AccountId);
                    if (account != null)
                    {
                        var address = _accountAddressReportDatabase.GetAccountAddress(command.DormId, command.AccountId);
                        Guid addressId;
                        if (address != null)
                        {
                            addressId = address.Id;
                        }
                        else
                        {
                            addressId = Guid.NewGuid();
                        }
                        var addressDomain = new AccountAddressDomain(addressId, command.FullName, command.Gender, account.MobilePhone,
                                command.DormId, string.Empty, command.AccountId, string.Empty, 0);
                        _addressRepository.Save(addressDomain);
                    }
                }
            }
        }
    }
}
