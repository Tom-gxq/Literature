using Grpc.Service.Core.Domain.Entity;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class SysKindDomain : AggregateRoot<Guid>
    {
        public int Kind { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double DiscountValue { get; set; }
        public double Amount { get; set; }
        public SysKindDomain()
        {

        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is SysKindEntity)
            {
                var entity = memento as SysKindEntity;
                this.Id = new Guid(entity.KindId);
                this.Kind = entity.Kind.Value;
                this.Price = entity.Price.Value;
                this.Quantity = entity.Quantity.Value;
                this.Unit = entity.Unit.Value;
                this.DiscountValue = entity.DiscountValue.Value;
                this.Description = entity.Description;
                this.Amount = entity.Amount != null ? entity.Amount.Value:0;
            }
        }
    }
}
