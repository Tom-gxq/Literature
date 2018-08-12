using Lib.EntityFramework.EntityFramework;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework.Repositories
{
    public class AccountProductRespository : RepositoryBase<AccountProductEntity, int>
    {
        public AccountProductRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public bool AddAccountProduct(AccountProductEntity data)
        {
            var result = this.Insert(data);
            return result > 0;
        }
    }
}
