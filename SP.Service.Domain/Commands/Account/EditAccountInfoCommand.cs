using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAccountInfoCommand : Command
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
        public int UserType { get; set; }
        public int DormId { get; set; }
        public EditAccountInfoCommand(string accountId,  string userName, bool gender, string avatar,int userType,int dormId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.Avatar = avatar;
            this.FullName = userName;
            this.Gender = gender ? 1:0;
            this.AccountId = accountId;
            this.UserType = userType;
            this.DormId = dormId;
            this.CommandType = CommandType.EditAccountInfo;
        }
    }
}
