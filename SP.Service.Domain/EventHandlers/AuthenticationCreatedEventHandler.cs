using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AuthenticationCreatedEventHandler:IEventHandler<AuthenticationCreatedEvent>
    {
        private readonly AuthenticationReportDatabase _reportDatabase;
        public AuthenticationCreatedEventHandler(AuthenticationReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AuthenticationCreatedEvent handle)
        {
            var item = new AuthenticationEntity()
            {
                AccountId = handle.AccountId,
                Account = handle.Account,
                AuthType = handle.AuthType,
                Token = handle.Token,
                VerifyCode = handle.VerifyCode,                
                Status = 0,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            _reportDatabase.Add(item);
        }
    }
}
