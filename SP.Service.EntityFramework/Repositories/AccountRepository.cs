using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AccountRepository : EfRepository<AccountEntity>
    {
        public AccountRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddAccount(AccountEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }
        public bool UpdateAccount(AccountEntity account)
        {
            var result = this.UpdateNonDefaults(account,x=>x.AccountId == account.AccountId );
            return result > 0;
        }

        public AccountEntity GetAccountById(string accountId)
        {
            return this.Single(x=>x.AccountId == accountId);
        }

        public AccountEntity GetAccount(string account)
        {
            return this.Single(x => x.MobilePhone == account || x.Email == account);
        }

        public AccountEntity GetAccountByAli(string otherAccount)
        {
            return this.Single(x => x.AliBind == otherAccount);
        }
        public AccountEntity GetAccountByWx(string otherAccount)
        {
            return this.Single(x => x.WxBind == otherAccount);
        }
        public AccountEntity GetAccountByQQ(string otherAccount)
        {
            return this.Single(x => x.QQBind == otherAccount);
        }

        public AccountEntity GetAccountByUnionId(string wxUnionId)
        {
            return this.Single(x => x.WxUnionId == wxUnionId);
        }
        public AccountEntity GetAccountByMobilePhone(string mobilePhone)
        {
            return this.Single(x => x.MobilePhone.Contains(mobilePhone));
        }
    }
}
