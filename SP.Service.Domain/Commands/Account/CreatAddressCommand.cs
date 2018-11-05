using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreatAddressCommand : Command
    {
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Mobile { get; set; }
        public int RegionID { get; set; }
        public string Address { get; set; }
        public string AccountId { get; set; }
        public string Dorm { get; set; }
        public int IsDefault { get; set; }
        public CreatAddressCommand(Guid id, string userName, int gender, string mobile, int regionId, string address, string accountId,string dorm, int isDefault) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.UserName = userName;
            this.Gender = gender;
            this.Mobile = mobile;
            this.RegionID = regionId;
            this.Address = address;
            this.AccountId = accountId;
            this.Dorm = dorm;
            this.IsDefault = isDefault;
            this.CommandType = CommandType.CreatAddress;
        }
    }
}
