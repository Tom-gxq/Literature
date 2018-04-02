using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountInfoCreatedEventHandler : IEventHandler<AccountInfoCreatedEvent>
    {
        private readonly AccountReportDatabase _reportDatabase;
        public AccountInfoCreatedEventHandler(AccountReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AccountInfoCreatedEvent handle)
        {
            var item = new AccountInfoEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                Avatar = !string.IsNullOrEmpty(handle.Avatar)? handle.Avatar:null,
                Fullname = !string.IsNullOrEmpty(handle.Fullname) ? handle.Fullname : null,
                Gender = handle.Gender ? 1:0,
                IM_QQ = !string.IsNullOrEmpty(handle.IM_QQ) ? handle.IM_QQ : null,
                WeiXin = !string.IsNullOrEmpty(handle.WeiXin) ? handle.WeiXin : null,
                UserType = handle.UserType,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.AddInfo(item);
        }
    }
}
