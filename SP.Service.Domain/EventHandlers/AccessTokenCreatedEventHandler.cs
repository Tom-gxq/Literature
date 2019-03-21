using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AccessTokenCreatedEventHandler : IEventHandler<AccessTokenCreatedEvent>, IEventHandler<AccessTokenDelEvent>
    {
        private readonly AccessTokenReportDatabase _reportDatabase;
        public AccessTokenCreatedEventHandler(AccessTokenReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AccessTokenCreatedEvent handle)
        {
            var item = new OAuth2AccessToken()
            {
                AccountId = handle.AccountId,
                AccessToken = handle.AccessToken,
                AccessTokenExpires = handle.AccessTokenExpires,
                RefreshToken = handle.RefreshToken,
                RefreshTokenExpires = handle.RefreshTokenExpires,                
                CreateTime = DateTime.Now,
            };

            _reportDatabase.Add(item);
        }

        public void Handle(AccessTokenDelEvent handle)
        {
            _reportDatabase.RemoveAccessToken(handle.AccessToken, handle.AccountId);
        }
    }
}
