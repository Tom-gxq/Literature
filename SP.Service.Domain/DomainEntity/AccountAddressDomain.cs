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
    public class AccountAddressDomain : AggregateRoot<Guid>,
        IHandle<AddressCreatedEvent>, IHandle<AddressEditEvent>,
        IOriginator
    {
        public  int AddressId { get; set; }
        public string UserName { get; set; }
        public int Gender { get; set; }
        public string Mobile { get; set; }
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        private string address { get; set; }
        public string Address
        {
            get
            {
                if (!string.IsNullOrEmpty(address))
                {
                    return address;
                }
                else
                {
                    return string.Format("{0} {1} {2}", this.SchoolName ?? string.Empty, this.DistrictName ?? string.Empty, this.Dorm ?? string.Empty);
                }
            }
            set
            {
                this.address = value;
            }
        }
        public string AccountId { get; set; }
        public string Dorm { get; set; }
        public int IsDefault { get; set; }

        public AccountAddressDomain()
        {

        }
        public AccountAddressDomain(Guid id, string userName, int gender, string mobile, int regionId, string address, string accountId,string dorm, int isDefault)
        {
            ApplyChange(new AddressCreatedEvent(id, userName, gender, mobile, regionId, address, accountId, dorm, isDefault));
        }

        public void EditAddressDomain(Guid id,int addressId, string userName, int gender, string mobile, int regionId, string address, string accountId, string dorm, int isDefault)
        {
            ApplyChange(new AddressEditEvent(id,addressId, userName, gender, mobile, regionId, address, accountId, dorm, isDefault));
        }
        public BaseEntity GetMemento()
        {
            return new AccountAddressEntity()
            {
                AccountId = this.AccountId,
                UserName = this.UserName,
                Gender = this.Gender,
                Mobile = this.Mobile,
                RegionID = this.DistrictId,
                Address = this.Address,
                Dorm = this.Dorm,
                IsDefault = this.IsDefault,
                ID = this.AddressId,
            };
        }

        public void Handle(AddressCreatedEvent e)
        {
            this.AccountId = e.AccountId.ToString();
            this.UserName = e.UserName;
            this.Gender = e.Gender;
            this.Mobile = e.Mobile;
            this.DistrictId = e.RegionID;
            this.Address = e.Address;
            this.Dorm = e.Dorm;
            this.IsDefault = e.IsDefault;
        }

        public void Handle(AddressEditEvent e)
        {
            this.AccountId = e.AccountId.ToString();
            this.UserName = e.UserName;
            this.Gender = e.Gender;
            this.Mobile = e.Mobile;
            this.DistrictId = e.RegionID;
            this.Address = e.Address;
            this.AddressId = e.AddressId;
            this.Dorm = e.Dorm;
            this.IsDefault = e.IsDefault;
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is AccountAddressEntity)
            {
                var entity = memento as AccountAddressEntity;
                this.AccountId = entity.AccountId;
                this.UserName = entity.UserName;
                this.Gender = entity.Gender.Value;
                this.Mobile = entity.Mobile;
                this.DistrictId = entity.RegionID!= null ?entity.RegionID.Value:0;
                this.Address = entity.Address;
                this.AddressId = entity.ID.Value;
                this.Dorm = entity.Dorm;
                this.IsDefault = entity.IsDefault != null ? entity.IsDefault.Value : 0;
            }
        }
    }
}
