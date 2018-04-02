using Grpc.Service.Core.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Account
{
    public class CreateAccessTokenCommand : Command
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public DateTime AccessTokenExpires { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public DateTime CreateTime { get; set; }
        public CreateAccessTokenCommand(Guid id,string accessToken, string accountId, DateTime accessTokenExpires, string refreshToken, DateTime refreshTokenExpires)
        {
            base.Id = id;
            this.AccessToken = accessToken;
            this.AccountId = accountId;
            this.AccessTokenExpires = accessTokenExpires;
            this.RefreshToken = refreshToken;
            this.RefreshTokenExpires = refreshTokenExpires;
            this.CreateTime = DateTime.Now;
        }
    }
}
