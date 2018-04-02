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
    public class AuthenticationDomain : AggregateRoot<Guid>,
        IHandle<AuthenticationCreatedEvent>, IHandle<AuthenticationEditEvent>,
        IOriginator
    {
        public int AuthType { get; set; }
        public string AccountId { get; set; }
        public string Account { get; set; }
        public string VerifyCode { get; set; }
        public string Token { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public AuthenticationDomain()
        {
        }

        public AuthenticationDomain(Guid id, int authType, string accountId, string account, string verifyCode, string token)
        {
            ApplyChange(new AuthenticationCreatedEvent(id, authType, accountId, account, verifyCode, token));
        }

        public void EditAuthenticationDomain(Guid id, int authType, string accountId, string account, int status)
        {
            ApplyChange(new AuthenticationEditEvent(id, authType, accountId, account, status));
        }
        public BaseEntity GetMemento()
        {
            return new AuthenticationEntity()
            {
                AccountId = this.AccountId,
                Account = this.Account,
                AuthType = this.AuthType,
                Status = this.Status,
                Token = this.Token,
                VerifyCode = this.VerifyCode,
                UpdateTime = this.UpdateTime,
                CreateTime = this.CreateTime
            };
        }

        public void Handle(AuthenticationCreatedEvent e)
        {
            this.AccountId = e.AccountId;
            this.Account = e.Account;
            this.AuthType = e.AuthType;
            this.Token = e.Token;
            this.VerifyCode = e.VerifyCode;            
        }

        public void Handle(AuthenticationEditEvent e)
        {
            this.AccountId = e.AccountId;
            this.Account = e.Account;
            this.AuthType = e.AuthType;
            this.Status = e.Status;
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is AuthenticationEntity)
            {
                var entity = memento as AuthenticationEntity;
                this.AccountId = entity.AccountId;
                this.Account = entity.Account;
                this.AuthType = entity.AuthType.Value;
                this.Token = entity.Token;
                this.VerifyCode = entity.VerifyCode;
                this.Status = entity.Status.Value;
                this.UpdateTime = entity.UpdateTime != null ? entity.UpdateTime.Value : DateTime.MinValue;
                this.CreateTime = entity.CreateTime.Value;
            }
        }
    }
}
