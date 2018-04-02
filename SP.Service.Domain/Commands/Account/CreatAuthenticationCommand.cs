using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreatAuthenticationCommand : Command
    {
        public int AuthType { get; set; }
        public string AccountId { get; set; }
        public string Account { get; set; }
        public string VerifyCode { get; set; }
        public string Token { get; set; }

        public CreatAuthenticationCommand(Guid id, int authType, string accountId, string account, string verifyCode, string token)
        {
            base.Id = id;
            this.AuthType = authType;
            this.AccountId = accountId;
            this.Account = account;
            this.VerifyCode = verifyCode;
            this.Token = token;
        }
    }
}
