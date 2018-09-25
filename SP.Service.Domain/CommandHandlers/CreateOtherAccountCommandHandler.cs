using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Producer;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreateOtherAccountCommandHandler : ICommandHandler<CreateOtherAccountCommand>
    {
        private IDataRepository<AccountDomain> _repository;
        private IDataRepository<SysStatisticsDomain> _sysRepository;
        private AccountReportDatabase _accountReportDatabase;
        private SysStatisticsReportDatabase _sysStatisticsReportDatabase;

        public CreateOtherAccountCommandHandler(IDataRepository<AccountDomain> repository, IDataRepository<SysStatisticsDomain> sysRepository,
            AccountReportDatabase accountReportDatabase,
            SysStatisticsReportDatabase sysStatisticsReportDatabase)
        {
            this._repository = repository;
            this._sysRepository = sysRepository;
            this._accountReportDatabase = accountReportDatabase;
            this._sysStatisticsReportDatabase = sysStatisticsReportDatabase;
        }

        public void Execute(CreateOtherAccountCommand command)
        {
            var account = _accountReportDatabase.GetAccount(command.MobilePhone);
            if (account == null)
            {
                var aggregate = new AccountDomain(command.Id, command.MobilePhone, command.OtherAccount, command.OtherType, command.FullName, command.Avatar, command.Gender);
                //统计
                aggregate.AddUserRegKafkaInfo(command.Id.ToString());
                _repository.Save(aggregate);

                //注册系统事件
                var eventList = _accountReportDatabase.GetDefaultEventList(0);
                foreach (var item in eventList)
                {
                    System.Console.WriteLine("CreateOtherAccountCommand item.KindId=" + item.KindId + "  AccountId=" + command.Id.ToString());
                    try
                    {
                        aggregate = new AccountDomain();
                        aggregate.CreateSysMember(command.Id.ToString(), item.KindId, item.Quantity);
                        //会员赠送统计
                        aggregate.AddMemberKafkaInfo(command.Id.ToString(), 0, AuthorizeType.Present);
                        _repository.Save(aggregate);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("CreateOtherAccountCommand ex=" + ex.Message + "\r\n  StackTrace=" + ex.StackTrace);
                    }
                }
            }
            else
            {
                var aggregate = new AccountDomain();
                aggregate.BindAccount(new Guid(account.AccountId), command.OtherAccount, command.OtherType);
                _repository.Save(aggregate);
            }

        }
    }
}
