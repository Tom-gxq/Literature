using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AccountFinanceRepository : EfRepository<AccountFinanceEntity>
    {
        public AccountFinanceRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public bool Add(AccountFinanceEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }

        public bool UpdateAccountFinance(AccountFinanceEntity account)
        {
            var result = this.UpdateNonDefaults(account,x=>x.AccountId == account.AccountId);
            return result > 0;
        }
        public AccountFinanceEntity GetAccountFinanceDetail(string accountId)
        {
            return this.Single(x=>x.AccountId == accountId);
        }
    }
}
