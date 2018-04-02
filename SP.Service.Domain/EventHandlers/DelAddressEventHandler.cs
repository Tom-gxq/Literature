using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class DelAddressEventHandler: IEventHandler<DelAddressEvent>
    {
        private readonly AddressReportDatabase _reportDatabase;
        public DelAddressEventHandler(AddressReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(DelAddressEvent handle)
        {
            _reportDatabase.RemoveAccountAddress(handle.AddressId);
        }
    }
}
