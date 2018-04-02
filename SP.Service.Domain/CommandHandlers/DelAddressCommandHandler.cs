using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class DelAddressCommandHandler : ICommandHandler<DelAddressCommand>
    {
        private AddressReportDatabase _accessTokenReportDatabase;

        public DelAddressCommandHandler(AddressReportDatabase accessTokenReportDatabase)
        {
            _accessTokenReportDatabase = accessTokenReportDatabase;
        }

        public void Execute(DelAddressCommand command)
        {
            _accessTokenReportDatabase.RemoveAccountAddress(command.AddressId);
        }
    }
}
