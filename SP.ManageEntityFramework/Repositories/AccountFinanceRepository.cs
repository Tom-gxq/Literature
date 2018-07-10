using Lib.EntityFramework.EntityFramework;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework.Repositories
{
    public class AccountFinanceRepository : RepositoryBase<AccountFinanceEntity, int>
    {
        public AccountFinanceRepository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public AccountFinanceEntity GetAccountFinanceDetail(string accountId)
        {
            return this.Single(x => x.AccountId == accountId);
        }
    }
}
