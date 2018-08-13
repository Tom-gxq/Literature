using Lib.EntityFramework.EntityFramework;
using ServiceStack.OrmLite;
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

        public bool DelShopOwner(int shopId, string accountId)
        {
            using (var db = Context.OpenDbConnection())
            {
                using (var cmd = db.SqlProc("DelShopOwner", new { ShopId = shopId, OwnerId = accountId }))
                {                     
                    var results = cmd.ExecuteNonQuery();
                }
            }
            return true;
        }
    }
}
