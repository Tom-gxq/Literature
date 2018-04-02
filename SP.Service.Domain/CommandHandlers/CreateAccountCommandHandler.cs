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
    public class CreateAccountCommandHandler: ICommandHandler<CreatAccountCommand>
    {
        private IDataRepository<AccountDomain> _repository;
        private AccountReportDatabase _accountReportDatabase;

        public CreateAccountCommandHandler(IDataRepository<AccountDomain> repository, AccountReportDatabase accountReportDatabase)
        {
            this._repository = repository;
            _accountReportDatabase = accountReportDatabase;
        }

        public void Execute(CreatAccountCommand command)
        {
            var aggregate = new AccountDomain(command.Id, command.MobilePhone, command.Email, command.Password, command.Status,command.UserName);
            _repository.Save(aggregate);
            var eventList = _accountReportDatabase.GetDefaultEventList(0);
            foreach(var item in eventList)
            {
                System.Console.WriteLine("item.KindId="+ item.KindId +"  AccountId="+ command.Id.ToString());
                try
                {
                    aggregate = new AccountDomain();
                    aggregate.CreateSysMember(command.Id.ToString(), item.KindId, item.Quantity);
                    _repository.Save(aggregate);
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine("ex="+ex.Message + "\r\n  StackTrace=" + ex.StackTrace);
                }
            }            
        }
    }
}
