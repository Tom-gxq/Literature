using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class AttributeDomain : AggregateRoot<int>,
        IOriginator
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        public int Type { get; set; }
        public int DisplaySequence { get; set; }

        public AttributeDomain()
        {

        }
        public BaseEntity GetMemento()
        {
            return new AttributeEntity()
            {
                 Id=this.Id,
                 AttributeName = this.AttributeName,
                 Type = this.Type,
                 DisplaySequence = this.DisplaySequence
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is AttributeEntity)
            {
                var entity = memento as AttributeEntity;
                this.Id = entity.Id;
                this.AttributeName = entity.AttributeName;
                this.Type = entity.Type.Value;
                this.DisplaySequence = entity.DisplaySequence.Value;
            }
        }
    }
}
