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
    public class CreateAccountIDCardCommandHandler: ICommandHandler<CreateAccountIDCardCommand>
    {
        private IDataRepository<AccountDomain> _repository;
        private IDataRepository<AccountAddressDomain> _addressRepository;
        private AccountReportDatabase _accountReportDatabase;
        private AddressReportDatabase _accountAddressReportDatabase;
        private static object lockObj = new object();
        private static object lockSecondObj = new object();

        public CreateAccountIDCardCommandHandler(IDataRepository<AccountDomain> repository, IDataRepository<AccountAddressDomain> addressRepository,
            AccountReportDatabase accountReportDatabase,
            AddressReportDatabase accountAddressReportDatabase)
        {
            this._repository = repository;
            this._accountReportDatabase = accountReportDatabase;
            this._accountAddressReportDatabase = accountAddressReportDatabase;
            this._addressRepository = addressRepository;
        }

        public void Execute(CreateAccountIDCardCommand command)
        {
            lock (lockObj)
            {
                lock (lockSecondObj)
                {
                    var aggregate = new AccountInfoDomain();
                    aggregate.EditAccountInfoDomain(command.Id.ToString(), command.FullName, 0, null, command.UserType);
                    _repository.Save(aggregate);

                    var account = _accountReportDatabase.GetAccountById(command.Id.ToString());
                    if (account != null)
                    {
                        var address = _accountAddressReportDatabase.GetDefaultSelectedAddress(command.Id.ToString());
                        AccountAddressDomain addressDomain = null;
                        if (address != null)
                        {
                            addressDomain = new AccountAddressDomain();
                            addressDomain.EditAddressDomain(command.Id, address.AddressId, address.UserName, address.Gender, 
                                address.Mobile, command.DormId, address.Address, address.AccountId, address.DormName, address.IsDefault);
                        }
                        else
                        {                            
                            addressDomain = new AccountAddressDomain(command.Id, command.FullName, 0, account.MobilePhone,
                                command.DormId, string.Empty, command.Id.ToString(), string.Empty, 0);
                        }
                        
                        _addressRepository.Save(addressDomain);
                    }
                }
            }
        }
    }
}
