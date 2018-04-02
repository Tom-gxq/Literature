using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountFinanceCreateEventHandler : IEventHandler<AccountFinanceCreateEvent>
    {
        private readonly AccountFinanceReportDatabase _reportDatabase;
        public AccountFinanceCreateEventHandler(AccountFinanceReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AccountFinanceCreateEvent handle)
        {
            var item = new AccountFinanceEntity()
            {
                AccountId = handle.AccountId,
                HaveAmount = handle.Amount
            };

            _reportDatabase.Add(item);
        }
    }
}
