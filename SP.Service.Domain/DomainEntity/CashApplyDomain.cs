using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class CashApplyDomain : AggregateRoot<Guid>,
        IHandle<CashApplyCreatedEvent>
    {
        public string AccountId { get; set; }
        public string Alipay { get; set; }
        public double Money { get; set; }
        public CashApplyDomain()
        {

        }
        public CashApplyDomain(string accountId, string alipay, double money)
        { 
            ApplyChange(new CashApplyCreatedEvent(this.Id,accountId, alipay, money));
        }
        public BaseEntity GetMemento()
        {
            return new CashApplyEntity()
            {
                AccountId = this.AccountId,
                Alipay = this.Alipay,
                Money = this.Money,
            };
        }

        public void Handle(CashApplyCreatedEvent e)
        {
            this.AccountId = e.AccountId;
            this.Alipay = e.Alipay;
            this.Money = e.Money;
        }
        public void SetMemento(BaseEntity memento)
        {
            if (memento is CashApplyEntity)
            {
                var entity = memento as CashApplyEntity;
                this.AccountId = entity.AccountId;
                this.Alipay = entity.Alipay;
                this.Money = entity.Money;
            }
        }
    }
}
