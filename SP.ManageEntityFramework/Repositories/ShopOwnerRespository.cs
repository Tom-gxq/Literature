using Lib.EntityFramework.EntityFramework;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework.Repositories
{
    public class ShopOwnerRespository : RepositoryBase<ShopOwnerEntity, int>
    {
        public ShopOwnerRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public bool AddShopOwner(ShopOwnerEntity data)
        {
            var result = this.Insert(data);
            return result > 0;
        }

        public ShopOwnerEntity GetShopOwnerByAccountId(string accountId)
        {
            return this.Single(x=>x.OwnerId == accountId);
        }
    }
}
