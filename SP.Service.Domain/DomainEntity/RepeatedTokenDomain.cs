using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Domain.Commands.Token;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class RepeatedTokenDomain : AggregateRoot<Guid>, 
        IHandle<TokenCreatedEvent>, IHandle<TokenDisabledEvent>
    {
        public string AccessToken { get;  set; }
        public string AccountId { get;  set; }
        public bool Status { get;  set; }
        public DateTime CreateTime { get;  set; }
        public DateTime UpdateTime { get; set; }

        public RepeatedTokenDomain()
        {            
        }

        public void Create()
        {
            var tokenEvent = TokenEventFactory.CreatTokenEvent(this);
            if (tokenEvent != null)
            {
                ApplyChange(tokenEvent);
            }
            else
            {
                throw new Exception("CreatTokenEvent failed");
            }
        }
        public void Update()
        {
            this.MarkChangesAsCommitted();
            this.SetAction( DaomainAction.Update);
            var tokenEvent = TokenEventFactory.CreatTokenEvent(this);
            ApplyChange(tokenEvent);
        }
        public override BaseEntity GetMemento()
        {
            return new RepeatedTokenEntity()
            {
                AccessToken = this.AccessToken,
                AccountID = this.AccountId,
                CreateTime = this.CreateTime,
                Status = this.Status
            };
        }

        public override void SetMemento(BaseEntity memento)
        {
            if (memento is RepeatedTokenEntity)
            {
                var entity = memento as RepeatedTokenEntity;
                this.AccessToken = entity.AccessToken;
                this.AccountId = entity.AccountID;
                this.CreateTime = entity.CreateTime.Value;
                this.Status = entity.Status.Value;
            }
        }
        public void Handle(TokenCreatedEvent e)
        {
            
        }
        public void Handle(TokenDisabledEvent e)
        {
            
        }
    }
}
