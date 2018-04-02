using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class AccessTokenDomain : AggregateRoot<Guid>,
        IHandle<AccessTokenCreatedEvent>,
        IOriginator
    {
        public string AccessToken { get; set; }
        public string AccountId { get; set; }
        public DateTime AccessTokenExpires { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
        public DateTime CreateTime { get; set; }

        public AccessTokenDomain()
        {

        }
        public AccessTokenDomain(string accountId, string accessToken, DateTime accessTokenExpires, string refreshToken, DateTime refreshTokenExpires, DateTime createTime)
        {
            ApplyChange(new AccessTokenCreatedEvent(accountId, accessToken, accessTokenExpires, refreshToken, refreshTokenExpires, createTime));
        }
        public BaseEntity GetMemento()
        {
            return new OAuth2AccessToken()
            {
                AccountId = this.AccountId,
                AccessToken = this.AccessToken,
                AccessTokenExpires = this.AccessTokenExpires,
                RefreshToken = this.RefreshToken,
                RefreshTokenExpires = this.RefreshTokenExpires,
                CreateTime = this.CreateTime
            };
        }

        public void Handle(AccessTokenCreatedEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.AccessToken = e.AccessToken;
            this.AccessTokenExpires = e.AccessTokenExpires;
            this.RefreshToken = e.RefreshToken;
            this.RefreshTokenExpires = e.RefreshTokenExpires;
            this.CreateTime = e.CreateTime;
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is OAuth2AccessToken)
            {
                var entity = memento as OAuth2AccessToken;
                this.AccountId = entity.AccountId;
                this.AccessToken = entity.AccessToken;
                this.AccessTokenExpires = entity.AccessTokenExpires.Value;
                this.RefreshToken = entity.RefreshToken;
                this.RefreshTokenExpires = entity.RefreshTokenExpires.Value;
                this.CreateTime = entity.CreateTime.Value;
            }
        }
    }
}
