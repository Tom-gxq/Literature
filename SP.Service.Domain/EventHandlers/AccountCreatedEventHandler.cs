using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
    {
        private readonly AccountReportDatabase _reportDatabase;
        public AccountCreatedEventHandler(AccountReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AccountCreatedEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                Email = handle.Email,
                MobilePhone = handle.MobilePhone,
                Password = handle.Password,
                Status = handle.Status,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Add(item);
        }
    }
}
