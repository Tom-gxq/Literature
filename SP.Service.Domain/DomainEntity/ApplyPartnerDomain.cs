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
    public class ApplyPartnerDomain : AggregateRoot<Guid>, IHandle<ApplyPartnerCreatedEvent>,
        IOriginator
    {
        public int DormId { get; set; }
        public ApplyPartnerDomain()
        {

        }
        public ApplyPartnerDomain(Guid accountId,int dormId)
        {
            ApplyChange(new ApplyPartnerCreatedEvent(accountId, dormId));
        }
        public void Handle(ApplyPartnerCreatedEvent e)
        {
            this.Id = e.AggregateId;
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is ApplyPartnerEntity)
            {
                var entity = memento as ApplyPartnerEntity;
                this.Id = new Guid(entity.AccountId);
                this.DormId = entity.DormId != null ? entity.DormId.Value:0;
            }
        }
        public BaseEntity GetMemento()
        {
            return new ApplyPartnerEntity()
            {
                AccountId = this.Id.ToString(),
                DormId = this.DormId,
            };
        }
    }
}
