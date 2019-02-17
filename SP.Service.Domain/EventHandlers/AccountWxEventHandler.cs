using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountWxEventHandler : IEventHandler<WxOpenIdCreateEvent>, IEventHandler<WxUnionIdEditEvent>
    {
        private readonly AccountReportDatabase _reportDatabase;
        public AccountWxEventHandler(AccountReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(WxOpenIdCreateEvent handle)
        {
            var item = new AccountOpenIdEntity()
            {
                AccountId = handle.AccountId,
                WxOpenId = handle.WxOpenId,
                WxType = handle.WxType, 
                CreateTime = handle.CreateTime,
            };

            _reportDatabase.AddOpenId(item);
        }
        public void Handle(WxUnionIdEditEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AccountId,
                UpdateTime = DateTime.Now,
                WxUnionId = handle.WxUnionId,
            };

            _reportDatabase.Update(item);
        }
    }
}
