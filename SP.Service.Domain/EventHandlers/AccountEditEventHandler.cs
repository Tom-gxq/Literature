using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountEditEventHandler : IEventHandler<AccountEditEvent>, IEventHandler<AliBindEvent>, 
        IEventHandler<WxBindEvent>, IEventHandler<QQBindEvent>
    {
        private readonly AccountReportDatabase _reportDatabase;
        public AccountEditEventHandler(AccountReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AccountEditEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                Email = handle.Email,
                MobilePhone = handle.MobilePhone,
                Password = handle.Password,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Update(item);
        }
        public void Handle(AliBindEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                AliBind = handle.OtherAccount,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Update(item);
        }
        public void Handle(WxBindEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                WxBind = handle.OtherAccount,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Update(item);
        }
        public void Handle(QQBindEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                QQBind = handle.OtherAccount,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Update(item);
        }
    }
}
