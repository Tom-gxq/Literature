using Grpc.Service.Core.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AddressEditEvent : Event
    {
        public int AddressId { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Mobile { get; set; }
        public int RegionID { get; set; }
        public string Address { get; set; }
        public string AccountId { get; set; }
        public string Dorm { get; set; }
        public int IsDefault { get; set; }
        public AddressEditEvent(Guid aggregateId, int addressId ,string userName, int gender, string mobile, int regionId, string address, string accountId,string dorm, int isDefault)
        {
            AggregateId = aggregateId;
            UserName = userName;
            Gender = gender;
            Mobile = mobile;
            RegionID = regionId;
            Address = address;
            AccountId = accountId;
            AddressId = addressId;
            Dorm = dorm;
            IsDefault = isDefault;
        }
    }
}
