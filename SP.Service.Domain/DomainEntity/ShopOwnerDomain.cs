using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class ShopOwnerDomain : AggregateRoot<int>,
        IOriginator
    {
        public int Id { get; set; }

        public int ShopId { get; set; }
        public string OwnerId { get; set; }
        public int Stock { get; set; }
        public bool ShopStatus { get; set; }

        public ShopOwnerDomain()
        {

        }

        public BaseEntity GetMemento()
        {
            return new ShopOwnerEntity()
            {
                ShopId = this.ShopId,
                OwnerId = this.OwnerId,
                ShopStatus = this.ShopStatus
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is ShopOwnerEntity)
            {
                var entity = memento as ShopOwnerEntity;
                this.ShopId = entity.ShopId.Value;
                this.OwnerId = entity.OwnerId;
                this.ShopStatus = entity.ShopStatus!= null? entity.ShopStatus.Value:false;
            }
        }
    }
}
