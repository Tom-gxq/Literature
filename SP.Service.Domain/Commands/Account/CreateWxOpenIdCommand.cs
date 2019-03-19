using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateWxOpenIdCommand : SPCommand
    {
        public string AccountId { get; set; }
        public string WxOpenId { get; set; }
        public int WxType { get; set; }

        public CreateWxOpenIdCommand(string accountId, string wxOpenId, int wxType=0) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.AccountId = accountId;
            this.WxOpenId = wxOpenId;
            this.WxType = wxType;
            this.CommandType = CommandType.CreateWxOpenId;
        }
    }
}
