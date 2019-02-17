using Grpc.Service.Core.Domain.Entity;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class CouponsDomain : AggregateRoot<Guid>
    {
        public string CouponId { get; set; }
        public string KindId { get; set; }
        public string AssociatorId { get; set; }
        public string AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double ModelAmount { get; set; }
        public string ModeDescription { get; set; }

        public CouponsDomain()
        {

        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is CouponsEntity)
            {
                var entity = memento as CouponsEntity;                
                this.CouponId = entity.CouponId;
                this.KindId = entity.KindId;
                this.AssociatorId = entity.AssociatorId;
                this.AccountId = entity.AccountId;
                this.StartDate = entity.StartDate.Value;
                this.EndDate = entity.EndDate.Value;
                this.Status = entity.Status != null ? entity.Status.Value : 0;
            }
            if (memento is CouponsFullEntity)
            {
                var entity = memento as CouponsFullEntity;
                this.Description = entity.Description;
                this.Amount = entity.Amount != null ? entity.Amount.Value : 0; ;
                this.ModelAmount = entity.ModelAmount != null ? entity.ModelAmount.Value : 0; ;
                this.ModeDescription = entity.ModeDescription;
            }
        }
    }
}
