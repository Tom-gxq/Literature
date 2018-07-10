using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountPayPwdEventHandler : IEventHandler<AccountPayPwdCreateEvent>, IEventHandler<AccountPayPwdEditEvent>
    {
        private readonly AccountReportDatabase _reportDatabase;
        public AccountPayPwdEventHandler(AccountReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AccountPayPwdCreateEvent handle)
        {
            var item = new AccountInfoEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                PayPassWord = handle.PayPwd,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.UpdateInfo(item);
        }
        public void Handle(AccountPayPwdEditEvent handle)
        {
            var item = new AccountInfoEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                PayPassWord = handle.PayPwd,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.UpdateInfo(item);
        }
    }
}
