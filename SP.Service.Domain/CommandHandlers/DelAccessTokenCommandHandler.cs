using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class DelAccessTokenCommandHandler : ICommandHandler<DelAccessTokenCommand>
    {
        private AccessTokenReportDatabase _accessTokenReportDatabase;

        public DelAccessTokenCommandHandler(AccessTokenReportDatabase accessTokenReportDatabase)
        {
            _accessTokenReportDatabase = accessTokenReportDatabase;
        }

        public void Execute(DelAccessTokenCommand command)
        {
            _accessTokenReportDatabase.RemoveAccessToken(command.AccessToken, command.AccountId);
        }
    }
}
