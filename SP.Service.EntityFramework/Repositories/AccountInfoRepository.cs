using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AccountInfoRepository : EfRepository<AccountInfoEntity>
    {
        public AccountInfoRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public AccountInfoEntity GetAccountInfoById(string accountId)
        {
            return this.Single(x => x.AccountId == accountId);
        }
        public bool AddAccountInfo(AccountInfoEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }
        public bool UpdateAccountFullInfo(AccountInfoEntity account)
        {
            var result = this.UpdateNonDefaults(account,x=>x.AccountId == account.AccountId);
            return result > 0;
        }
    }
}
