using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class TradeDomain : AggregateRoot<Guid>, IHandle<TradeCreateEvent>
    {
        public string AccountId { get; internal set; }
        public string CartId { get; internal set; }
        public int Subject { get; internal set; }
        public double Amount { get; internal set; }
        public DateTime CreateTime { get; internal set; }
        public int Quantity { get; internal set; }
        public int ShipOrderId { get; internal set; }

        public TradeDomain()
        {
            
        }
        public TradeDomain(string accountId,string cartId,int subject,double amount,int shipOrderId=0)
        {
            ApplyChange(new TradeCreateEvent(Guid.NewGuid(), accountId, cartId,subject, amount, shipOrderId));
        }
        

        public BaseEntity GetMemento()
        {
            return new TradeEntity()
            {
                AccountId = this.AccountId,
                CartId = this.CartId,
                Subject = this.Subject,
                TradeId = this.Id.ToString(),
                Amount = this.Amount,
                CreateTime = this.CreateTime
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is TradeFullEntity)
            {
                var entity = memento as TradeFullEntity;
                this.AccountId = entity.AccountId;
                this.CartId = entity.CartId;
                this.Subject = entity.Subject.Value;
                this.Id = new Guid(entity.TradeId);
                this.Amount = entity.Amount != null ? entity.Amount.Value:0;
                this.CreateTime = entity.CreateTime.Value;
                this.Quantity = entity.Quantity;
            }
        }
        public void SetBaseMemento(BaseEntity memento)
        {
            if (memento is TradeEntity)
            {
                var entity = memento as TradeEntity;
                this.AccountId = entity.AccountId;
                this.CartId = entity.CartId;
                this.Subject = entity.Subject.Value;
                this.Id = new Guid(entity.TradeId);
                this.Amount = entity.Amount != null ? entity.Amount.Value : 0;
                this.CreateTime = entity.CreateTime.Value;
            }
        }
        public void Handle(TradeCreateEvent e)
        {
            this.AccountId = e.AccountId;
            this.CartId = e.CartId;
            this.Subject = e.Subject;
            this.Amount = e.Amount;
        }
    }
}
