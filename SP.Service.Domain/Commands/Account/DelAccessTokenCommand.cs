using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class DelAccessTokenCommand : SPCommand
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public DelAccessTokenCommand(Guid id, string accessToken, string accountId) : base(KafkaConfig.NormalCommandBusTopicTitle)
        {
            base.Id = id;
            this.AccessToken = accessToken;
            this.AccountId = accountId;
            this.CommandType = CommandType.DelAccessToken;
        }
    }
}
