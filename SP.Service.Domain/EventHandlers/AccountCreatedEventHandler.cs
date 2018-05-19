using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>, IEventHandler<AliBindCreatedEvent>, 
        IEventHandler<WxBindCreatedEvent>,  IEventHandler<QQBindCreatedEvent>
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
        public void Handle(AliBindCreatedEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AggregateId.ToString(),               
                MobilePhone = handle.MobilePhone,
                Status = handle.Status,
                AliBind = handle.OtherAccount,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Add(item);
        }
        public void Handle(WxBindCreatedEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                MobilePhone = handle.MobilePhone,
                Status = handle.Status,
                AliBind = handle.OtherAccount,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Add(item);
        }
        public void Handle(QQBindCreatedEvent handle)
        {
            var item = new AccountEntity()
            {
                AccountId = handle.AggregateId.ToString(),
                MobilePhone = handle.MobilePhone,
                Status = handle.Status,
                AliBind = handle.OtherAccount,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Add(item);
        }
    }
}
