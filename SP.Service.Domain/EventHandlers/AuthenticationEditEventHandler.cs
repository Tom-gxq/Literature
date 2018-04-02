using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AuthenticationEditEventHandler : IEventHandler<AuthenticationEditEvent>
    {
        private readonly AuthenticationReportDatabase _reportDatabase;
        public AuthenticationEditEventHandler(AuthenticationReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AuthenticationEditEvent handle)
        {
            var item = new AuthenticationEntity()
            {                
                AccountId = handle.AccountId,
                Account = handle.Account,
                AuthType = handle.AuthType,
                Status = handle.Status,
                UpdateTime = DateTime.Now
            };

            _reportDatabase.UpdateAuthenticationStatus(item);
        }
    }
}
