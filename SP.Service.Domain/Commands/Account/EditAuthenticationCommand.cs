using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class EditAuthenticationCommand : Command
    {
        public int AuthType { get; set; }
        public string AccountId { get; set; }
        public string Account { get; set; }
        public int Status { get; set; }
        public EditAuthenticationCommand(Guid id, int authType, string accountId, string account, int status)
        {
            base.Id = id;
            this.AuthType = authType;
            this.AccountId = accountId;
            this.Account = account;
            this.Status = status;
        }
    }
}
