using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class AccountFinanceDomain : AggregateRoot<Guid>, IHandle<AccountFinanceCreateEvent>
        , IHandle<HaveAmountEditEvent>, IHandle<UseAmountEditEvent>
    {
        public string AccountId { get; internal set; }
        public double HaveAmount { get; internal set; }
        public double UseAmount { get; internal set; }
        public double ConsumeAmount { get; internal set; }
        public AccountFinanceDomain()
        {

        }
        public AccountFinanceDomain(string accountId)
        {
            ApplyChange(new AccountFinanceCreateEvent(this.Id,accountId,0));
        }
        public AccountFinanceDomain(string accountId,double haveAmount)
        {
            ApplyChange(new AccountFinanceCreateEvent(this.Id,accountId, haveAmount));
        }
        public void EditHaveAmount(Guid id,string accountId,double amount)
        {
            ApplyChange(new HaveAmountEditEvent(id,accountId,amount));
        }
        public void EditUseAmount(Guid id,string accountId, double amount)
        {
            ApplyChange(new UseAmountEditEvent(id,accountId,amount));
        }
        public void Handle(AccountFinanceCreateEvent e)
        {
            this.AccountId = e.AccountId;
            this.HaveAmount = 0;
            this.UseAmount = 0;
        }
        public void Handle(HaveAmountEditEvent e)
        {
            this.AccountId = e.AccountId;
            this.HaveAmount += e.Amount;
        }
        public void Handle(UseAmountEditEvent e)
        {
            this.AccountId = e.AccountId;
            this.UseAmount += e.Amount;
        }

        public BaseEntity GetMemento()
        {
            return new AccountFinanceEntity()
            {
                AccountId = this.AccountId,
                HaveAmount = this.HaveAmount ,
                UseAmount = this.UseAmount,
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is AccountFinanceEntity)
            {
                var entity = memento as AccountFinanceEntity;
                this.AccountId = entity.AccountId;
                this.HaveAmount = entity.HaveAmount != null ? entity.HaveAmount.Value:0;
                this.UseAmount = entity.UseAmount!=null? entity.UseAmount.Value:0;
                this.ConsumeAmount = entity.ConsumeAmount != null ? entity.ConsumeAmount.Value : 0;
            }
        }
    }
}
