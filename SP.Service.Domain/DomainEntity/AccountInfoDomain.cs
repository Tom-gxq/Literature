using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class AccountInfoDomain : AggregateRoot<Guid>, IHandle<AccountInfoEditEvent>
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        public string Avatar { get; set; }
        public string Fullname { get; set; }
        public int UserType { get; set; }
        public int Gender { get; set; }
        public string IM_QQ { get; set; }
        public string WeiXin { get; set; }
        public string PayPassWord { get; set; }

        public AccountInfoDomain()
        {

        }
        public void EditAccountInfoDomain(string accountId, string userName, int gender, string avatar,int userType)
        {
            ApplyChange(new AccountInfoEditEvent(accountId, userName, gender, avatar,userType));
            //ApplyChange(new AddressCreatedEvent(Guid.NewGuid(), userName, gender, string.Empty, dormId, string.Empty, accountId, string.Empty, 0));
        }
        public void SetAccountPayPwd(string accountId, string payPwd)
        {
            ApplyChange(new AccountPayPwdCreateEvent(accountId, payPwd));            
        }
        public void EditAccountPayPwd(string accountId, string payPwd)
        {
            ApplyChange(new AccountPayPwdEditEvent(accountId, payPwd));            
        }

        public BaseEntity GetMemento()
        {
            return new AccountInfoEntity()
            {
                Id = this.Id,
                AccountId = this.AccountId,
                Avatar = this.Avatar,
                Fullname = this.Fullname,
                UserType = this.UserType,
                Gender = this.Gender,
                IM_QQ = this.IM_QQ,
                WeiXin = this.WeiXin,
                PayPassWord = this.PayPassWord
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is AccountInfoEntity)
            {
                var entity = memento as AccountInfoEntity;
                this.Id = entity.Id.Value;
                this.AccountId = entity.AccountId;
                this.Avatar = entity.Avatar;
                this.Fullname = entity.Fullname;
                this.UserType = entity.UserType.Value;
                this.Gender = entity.Gender.Value;
                this.IM_QQ = entity.IM_QQ;
                this.WeiXin = entity.WeiXin;
                this.PayPassWord = entity.PayPassWord;
            }
        }
        public void Handle(AccountInfoEditEvent e)
        {
            this.AccountId = e.AccountId;
            this.Avatar = e.Avatar;
            this.Fullname = e.FullName;
            this.Gender = e.Gender;
        }
        public void Handle(AccountPayPwdCreateEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.PayPassWord = e.PayPwd;
        } 
        public void Handle(AccountPayPwdEditEvent e)
        {
            this.AccountId = e.AggregateId.ToString();
            this.PayPassWord = e.PayPwd;
        }
    }
}
