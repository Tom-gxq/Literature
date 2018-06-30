using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using SP.Producer;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class AccountDomain : AggregateRoot<Guid>,
        IHandle<AccountCreatedEvent>, IHandle<AccountInfoCreatedEvent>, IHandle<AssociatorCreatedEvent>,
        IHandle<AccountEditEvent>, IHandle<KafkaAddEvent>,
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
        public string AliBind { get; set; }
        public string WxBind { get; set; }
        public string QQBind { get; set; }

        public AccountDomain()
        {

        }
        public AccountDomain(Guid accountId, string mobilePhone, string email, string password, int status,string userName)
        {
            ApplyChange(new AccountCreatedEvent(accountId, mobilePhone, email, password, status));
            ApplyChange(new AccountInfoCreatedEvent(accountId,fullname: userName));
        }
        public AccountDomain(Guid accountId, string mobilePhone, string otherAccount, OtherType otherType, string userName,string avatar,bool gender)
        {            
            if (otherType == OtherType.AliAccount)
            {
                ApplyChange(new AliBindCreatedEvent(accountId, mobilePhone,otherAccount));
            }
            else if (otherType == OtherType.WxAccount)
            {
                ApplyChange(new WxBindCreatedEvent(accountId, mobilePhone, otherAccount));
            }
            else if (otherType == OtherType.QQAccount)
            {
                ApplyChange(new QQBindCreatedEvent(accountId, mobilePhone, otherAccount));
            }
            else
            {
                throw new Exception();
            }
            ApplyChange(new AccountInfoCreatedEvent(accountId, avatar, userName, gender: gender));
        }
        public void CreateSysMember(string accountId,string kindId,int quantity)
        {
            var payType = 1;
            var payOrderCode = "ZSHY"+DateTime.Now.ToString("yyyyMMddHH24mmssffff");
            ApplyChange(new AssociatorCreatedEvent(Guid.NewGuid(),accountId, kindId, quantity, payOrderCode, payType,0));
        }
        public void EditAccount(Guid accountId, string mobilePhone, string email, string password)
        {
            ApplyChange(new AccountEditEvent(accountId, mobilePhone, email, password));
        }
        public void EditAccountPwd(Guid accountId, string password)
        {
            ApplyChange(new AccountEditEvent(accountId, null, null, password));
        }
        public void EditAccountMobile(Guid accountId, string mobilePhone)
        {
            ApplyChange(new AccountEditEvent(accountId, mobilePhone, null, null));
        }
        public void BindAccount(Guid accountId, string otherAccount, OtherType otherType)
        {
            if(otherType == OtherType.AliAccount)
            {
                ApplyChange(new AliBindEvent(accountId, otherAccount));
            }
            else if(otherType == OtherType.WxAccount)
            {
                ApplyChange(new WxBindEvent(accountId, otherAccount));
            }
            else if (otherType == OtherType.QQAccount)
            {
                ApplyChange(new QQBindEvent(accountId, otherAccount));
            }
            else
            {
                throw new Exception();
            }
        }
        public void AddUserRegKafkaInfo(string accountId)
        {
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();
            string kafkaIP = string.Empty;
            if (config != null)
            {
                kafkaIP = config.GetSection("KafkaIP").Value?.ToString() ?? string.Empty;
            }
            var producer = new KafkaUserRegProducer();
            producer.IPConfig = kafkaIP;
            producer.AccountId = accountId;
            ApplyChange(new KafkaAddEvent(producer));
        }
        public void AddMemberKafkaInfo(string accountId,double amount, AuthorizeType Type)
        {
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();
            string kafkaIP = string.Empty;
            if (config != null)
            {
                kafkaIP = config.GetSection("KafkaIP").Value?.ToString() ?? string.Empty;
            }
            var producer = new KafkaMemberProducer();
            producer.IPConfig = kafkaIP;
            producer.AccountId = accountId;
            producer.Amount = amount;
            producer.Type = Type;
            ApplyChange(new KafkaAddEvent(producer));
        }
        public void Handle(KafkaAddEvent e)
        {

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
        public void Handle(AccountEditEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.Email = e.Email;
            this.MobilePhone = e.MobilePhone;
            this.Password = e.Password;
        }
        public void Handle(AliBindEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.AliBind = e.OtherAccount;
        }
        public void Handle(WxBindEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.WxBind = e.OtherAccount;
        }
        public void Handle(QQBindEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.QQBind = e.OtherAccount;
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
                this.AliBind = entity.AliBind;
                this.WxBind = entity.WxBind;
                this.QQBind = entity.QQBind;
            }
        }
    }
    public enum OtherType
    {
        AliAccount=0,
        WxAccount,
        QQAccount
    }
}
