using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAuthenticationCommand : SPCommand
    {
        public int AuthType { get; set; }
        public string AccountId { get; set; }
        public string Account { get; set; }
        public int Status { get; set; }
        public EditAuthenticationCommand(Guid id, int authType, string accountId, string account, int status) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.AuthType = authType;
            this.AccountId = accountId;
            this.Account = account;
            this.Status = status;
            this.CommandType = CommandType.EditAuthentication;
        }
    }
}
