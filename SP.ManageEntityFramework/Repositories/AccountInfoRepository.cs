using Lib.EntityFramework.EntityFramework;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework.Repositories
{
    public class AccountInfoRepository : RepositoryBase<AccountInfoEntity, int>
    {
        public AccountInfoRepository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public AccountInfoEntity GetAccountInfoById(string accountId)
        {
            return this.Single(x => x.AccountId == accountId);
        }
        
        public bool UpdateAccountFullInfo(AccountInfoEntity account)
        {
            var result = this.UpdateNonDefaults(account, x => x.AccountId == account.AccountId);
            return result > 0;
        }
    }
}
