using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditWxUnionIdCommand : SPCommand
    {
        public string AccountId { get; set; }
        public string WxUnionId { get; set; }
        public EditWxUnionIdCommand(string accountId, string wxUnionId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            this.AccountId = accountId;
            this.WxUnionId = wxUnionId;
            this.CommandType = CommandType.EditWxUnionId;
        }
    }
}
