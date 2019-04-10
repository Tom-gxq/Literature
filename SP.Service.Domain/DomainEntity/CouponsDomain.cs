using Grpc.Service.Core.Domain.Entity;
using SP.Service.Domain.Events;
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
        public string AccountId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public double ModelAmount { get; set; }
        public string ModeDescription { get; set; }
        public string PayOrderCode { get; set; }
        public int PayType { get; set; }
        public int PayStatus { get; set; }
        public double PayAmount { get; set; }

        public CouponsDomain()
        {

        }
        public void Create(int num)
        {
            for (int i = 0; i < num; i++)
            {
                var @event = new CouponCreatedEvent(this.Id, this.KindId, this.AccountId, this.StartDate, this.EndDate,this.PayOrderCode);

                ApplyChange(@event);
            }
        }

        public void Payed()
        {
            var @event = new CouponPayedEvent(this.Id, this.PayAmount, this.PayType);

            ApplyChange(@event);
        }

        public void Used()
        {
            var @event = new CouponUsedEvent(this.Id, this.Status);

            ApplyChange(@event);
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is CouponsEntity)
            {
                var entity = memento as CouponsEntity;                
                this.CouponId = entity.CouponId;
                this.KindId = entity.KindId;
                this.AccountId = entity.AccountId;
                this.StartDate = entity.StartDate.Value;
                this.EndDate = entity.EndDate.Value;
                this.Status = entity.Status != null ? entity.Status.Value : 0;
                this.PayType = entity.PayType != null ? entity.PayType.Value : 0;
                this.PayStatus = entity.PayStatus != null ? entity.PayStatus.Value : 0;
                this.PayOrderCode = entity.PayOrderCode;
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
