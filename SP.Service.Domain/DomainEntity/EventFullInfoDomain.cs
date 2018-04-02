using Grpc.Service.Core.Domain.Entity;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class EventFullInfoDomain : AggregateRoot<Guid>
    {
        public int SysEventId { get; set; }
        public string SysEventName { get; set; }
        public int ResEventId { get; set; }
        public string ResEventName { get; set; }
        public int Quantity { get; set; }
        public string KindId { get; set; }
        public string Description { get; set; }
        public EventFullInfoDomain()
        {

        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is EventFullInfoEntity)
            {
                var entity = memento as EventFullInfoEntity;
                this.Id = new Guid(entity.KindId);
                this.KindId = entity.KindId;
                this.SysEventId = entity.SysEventId;
                this.SysEventName = entity.SysEventName;
                this.Quantity = entity.Quantity;
                this.ResEventId = entity.ResEventId;
                this.ResEventName = entity.ResEventName;
                this.Description = entity.Description;
            }
        }
    }
}
