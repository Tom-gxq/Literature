using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class ShopDomain : AggregateRoot<int>,
        IOriginator
    {
        public int Id { get; set; }

        public string ShopName { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int RegionId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ShopLogo { get; set; }

        public ShopDomain()
        {

        }

        public BaseEntity GetMemento()
        {
            return new ShopEntity()
            {
                Id = this.Id,
                ShopName = this.ShopName,
                OwnerId = this.OwnerId,
                RegionId = this.RegionId,
                StartTime = this.StartTime,
                EndTime = this.EndTime,
                ShopLogo = this.ShopLogo
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is ShopEntity)
            {
                var entity = memento as ShopEntity;
                this.Id = entity.Id.Value;
                this.ShopName = entity.ShopName;
                this.OwnerId = entity.OwnerId;
                this.RegionId = entity.RegionId!= null ? entity.RegionId.Value:0;
                this.StartTime = entity.StartTime;
                this.EndTime = entity.EndTime;
                this.ShopLogo = entity.ShopLogo;
            }
        }
    }
}
