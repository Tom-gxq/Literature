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

        public AccountEntity GetAccountById(string accountId)
        {
            return this.Single(x=>x.AccountId == accountId);
        }

        public AccountEntity GetAccount(string account)
        {
            return this.Single(x => x.MobilePhone == account || x.Email == account);
        }
    }
}
