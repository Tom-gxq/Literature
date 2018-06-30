using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers
{
    public class AddressEditEventHandler : IEventHandler<AddressEditEvent>
    {
        private readonly AddressReportDatabase _reportDatabase;
        public AddressEditEventHandler(AddressReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(AddressEditEvent handle)
        {
            int? regionId = null;
            if(handle.RegionID > 0)
            {
                regionId = handle.RegionID;
            }
            var item = new AccountAddressEntity()
            {
                ID = handle.AddressId,
                AccountId = handle.AccountId,
                Address = !string.IsNullOrEmpty(handle.Address) ? handle.Address : null,
                Gender = handle.Gender,
                Mobile = !string.IsNullOrEmpty(handle.Mobile) ? handle.Mobile : null,
                RegionID = regionId,
                UserName = !string.IsNullOrEmpty(handle.UserName) ? handle.UserName : null,
                Dorm = !string.IsNullOrEmpty(handle.Dorm) ? handle.Dorm : null,
                IsDefault = handle.IsDefault
            };

            _reportDatabase.UpdateAccountAddress(item);
        }
    }
}
