using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountInfoEditEventHandler : IEventHandler<AccountInfoEditEvent>
    {
        private readonly AccountInfoReportDatabase _reportDatabase;
        public AccountInfoEditEventHandler(AccountInfoReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AccountInfoEditEvent handle)
        {
            var item = new AccountInfoEntity()
            {
                AccountId = handle.AccountId,
                Gender = handle.Gender,
                Avatar  = handle.Avatar,
                Fullname = handle.FullName,
                UpdateTime = DateTime.Now
            };

            _reportDatabase.UpdateAccountFullInfo(item);
        }
    }
}
