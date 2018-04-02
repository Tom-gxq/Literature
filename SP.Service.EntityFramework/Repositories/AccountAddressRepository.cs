using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AccountAddressRepository : EfRepository<AccountAddressEntity>
    {
        public AccountAddressRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddAccountAddress(AccountAddressEntity accountAddress)
        {
            var result = this.Insert(accountAddress);
            return result > 0;
        }

        public bool EditAccountAddress(AccountAddressEntity accountAddress)
        {
            var result = this.UpdateNonDefaults(new AccountAddressEntity()
            {
                AccountId = accountAddress.AccountId,
                Address = accountAddress.Address,
                Gender = accountAddress.Gender,
                Mobile = accountAddress.Mobile,
                RegionID = accountAddress.RegionID,
                UserName = accountAddress.UserName
            },x=>x.ID == accountAddress.ID);
            return result > 0;
        }

        public bool UpdateAddressStatus(int id, int status)
        {
            var result = this.UpdateNonDefaults(new AccountAddressEntity()
            {
                IsDefault = status
            }, x => x.ID == id);
            return result > 0;
        }

        public List<AccountAddressEntity> GetAddressList(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<AccountAddressEntity>();
                q = q.Where(a => a.AccountId == accountId).OrderByDescending(x=>x.IsDefault);
                return db.Select(q);
            }
        }

        public List<AccountAddressEntity>  GetDefaultSelectedAddress(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<AccountAddressEntity>();
                q = q.Where(a => a.AccountId == accountId && a.IsDefault==1).OrderByDescending(x => x.IsDefault);
                return db.Select(q);
            }
        }

        public AccountAddressEntity GetAddressById(int addressId, string accountId)
        {
            var result = this.Single(x =>x.ID == addressId && x.AccountId == accountId);
            return result;
        }

        public bool RemoveAccountAddress(int addressId)
        {
            var result = this.Delete(x => x.ID == addressId);
            return result > 0;
        }

        public int UpdateAccountAddress(AccountAddressEntity entity)
        {
            return this.UpdateNonDefaults(entity, x => x.ID == entity.ID && x.AccountId == entity.AccountId);
        }

        public int EnableAccountAddress(AccountAddressEntity entity)
        {
            return this.UpdateNonDefaults(entity, x => x.IsDefault == 1 && x.AccountId == entity.AccountId);
        }
    }
}
