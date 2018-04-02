using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class DiscountDomain : AggregateRoot<Guid>
    {
        public string AccountId { get; set; }
        public string KindId { get; set; }
        public int Quantity { get; set; }
        public string PayOrderCode { get; set; }
        public int PayType { get; set; }
        public double Amount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public double DiscountValue { get; set; }
        public DiscountDomain()
        {

        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is DiscountEntity)
            {
                var entity = memento as DiscountEntity;
                this.AccountId = entity.AccountId;
                this.Id = new Guid(entity.AssociatorId);                
                this.Amount = entity.Amount.Value;
                this.PayOrderCode = entity.PayOrderCode;
                this.PayType = entity.PayType.Value;
                this.StartDate = entity.StartDate.Value;
                this.EndDate = entity.EndDate.Value;
                this.Quantity = entity.Quantity.Value;
                this.Description = entity.Description;
                this.DiscountValue = entity.DiscountValue.Value;
            }
        }
    }
}
