using Grpc.Service.Core.Domain.Events;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AddressCreatedEvent : Event
    {
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Mobile { get; set; }
        public int RegionID { get; set; }
        public string Address { get; set; }
        public string AccountId { get; set; }
        public string Dorm { get; set; }
        public int IsDefault { get; set; }
        public AddressCreatedEvent(Guid aggregateId, string userName, int gender, string mobile, int regionId, string address, string accountId,string dorm, int isDefault)
            : base(KafkaConfig.EventBusTopicTitle)
        {
            AggregateId = aggregateId;
            CommandId = aggregateId.ToString();
            UserName = userName;
            Gender = gender;
            Mobile = mobile;
            RegionID = regionId;
            Address = address;
            AccountId = accountId;
            Dorm = dorm;
            IsDefault = isDefault;
            EventType = EventType.AddressCreated;
        }
    }
}
