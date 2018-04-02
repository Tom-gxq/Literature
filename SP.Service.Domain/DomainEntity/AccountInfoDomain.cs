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

        public AccountInfoDomain()
        {

        }
        public void EditAccountInfoDomain(string accountId, string userName, int gender, string avatar)
        {
            ApplyChange(new AccountInfoEditEvent(accountId, userName, gender, avatar));
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
                WeiXin = this.WeiXin
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
            }
        }
        public void Handle(AccountInfoEditEvent e)
        {
            this.AccountId = e.AccountId;
            this.Avatar = e.Avatar;
            this.Fullname = e.FullName;
            this.Gender = e.Gender;
        }
    }
}
