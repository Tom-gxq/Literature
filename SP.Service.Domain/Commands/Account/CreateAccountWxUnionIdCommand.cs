using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateAccountWxUnionIdCommand : SPCommand
    {
        public string MobilePhone { get; set; }
        public string WxUnionId { get; set; }
        public int WxType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
        public string WxBind { get; set; }

        public CreateAccountWxUnionIdCommand(string mobilePhone, string wxUnionId, int wxType, string nickName,
            string avatarUrl,string passWord, int gender,string wxOpenId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.MobilePhone = mobilePhone;
            this.WxUnionId = wxUnionId;
            this.WxType = wxType;
            this.UserName = nickName;
            this.Avatar = avatarUrl;
            this.Gender = gender;
            this.Password = passWord;
            this.WxBind = wxOpenId;
            this.CommandType = CommandType.CreateAccountWxUnionId;
        }
    }
}
