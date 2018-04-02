using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AddressCreatedEventHandler : IEventHandler<AddressCreatedEvent>
    {
        private readonly AddressReportDatabase _reportDatabase;
        public AddressCreatedEventHandler(AddressReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AddressCreatedEvent handle)
        {
            var item = new AccountAddressEntity()
            {
                AccountId = handle.AccountId,
                Address = handle.Address,
                Gender = handle.Gender,
                Mobile  = handle.Mobile,
                RegionID = handle.RegionID,
                UserName = handle.UserName,
            };

            _reportDatabase.Add(item);
        }
    }
}
