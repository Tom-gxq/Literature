using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class DelAccessTokenCommand : Command
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public DelAccessTokenCommand(Guid id, string accessToken, string accountId)
        {
            base.Id = id;
            this.AccessToken = accessToken;
            this.AccountId = accountId;
        }
    }
}
