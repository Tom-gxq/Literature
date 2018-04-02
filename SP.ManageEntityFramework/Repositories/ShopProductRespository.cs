using Lib.EntityFramework.EntityFramework;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework.Repositories
{
    public class ShopProductRespository : RepositoryBase<ShopProductEntity, int>
    {
        public ShopProductRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public bool DelShopProductByShopId(int ShopId)
        {
            var result = this.Delete(x=>x.ShopId == ShopId);
            return result > 0;
        }
    }
}
