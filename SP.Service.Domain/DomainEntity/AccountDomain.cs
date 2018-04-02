using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class AccountDomain : AggregateRoot<Guid>,
        IHandle<AccountCreatedEvent>, IHandle<AccountInfoCreatedEvent>, IHandle<AssociatorCreatedEvent>,
        IOriginator
    {
        public string AccountId { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public AccountDomain()
        {

        }
        public AccountDomain(Guid accountId, string mobilePhone, string email, string password, int status,string userName)
        {
            ApplyChange(new AccountCreatedEvent(accountId, mobilePhone, email, password, status));
            ApplyChange(new AccountInfoCreatedEvent(accountId,fullname: userName));
        }
        public void CreateSysMember(string accountId,string kindId,int quantity)
        {
            var payType = 1;
            var payOrderCode = "ZSHY"+DateTime.Now.ToString("yyyyMMddHH24mmssffff");
            ApplyChange(new AssociatorCreatedEvent(Guid.NewGuid(),accountId, kindId, quantity, payOrderCode, payType,0));
        }
        public BaseEntity GetMemento()
        {
            return new AccountEntity()
            {
                AccountId = this.AccountId,
                MobilePhone = this.MobilePhone,
                Email = this.Email,
                Password = this.Password,
                Status = this.Status                
            };
        }

        public void Handle(AccountCreatedEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.MobilePhone = e.MobilePhone;
            this.Email = e.Email;
            this.Password = e.Password;
            this.Status = e.Status;
        }
        public void Handle(AccountInfoCreatedEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.UserName = e.Fullname;
        }
        public void Handle(AssociatorCreatedEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is AccountEntity)
            {
                var entity = memento as AccountEntity;
                this.AccountId = entity.AccountId;
                this.MobilePhone = entity.MobilePhone;
                this.Email = entity.Email;
                this.Password = entity.Password;
                this.Status = entity.Status.Value;
            }
        }
    }
}
