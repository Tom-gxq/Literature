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
    public class ShopRespository : RepositoryBase<ShopEntity, int>
    {
        public ShopRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<ShopEntity> GetShopList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShopEntity>().OrderBy(x => x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public List<ShopEntity>  GetShopListByRegionId(int regionId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShopEntity>().Where(x=>x.RegionId == regionId);
                q = q.OrderBy(x => x.DisplaySequence);
                return db.Select(q);
            }
        }
        public List<ShopEntity> GetAllShopList()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShopEntity>().OrderBy(x => x.DisplaySequence);
                return db.Select(q);
            }
        }

        public List<ShopEntity> SearchShopByName(string keyWord, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShopEntity>().Where(x => x.MetaKeywords.Contains(keyWord)).OrderBy(x => x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public int SearchShopByNameCount(string keyWord)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ShopEntity>().Where(x => x.MetaKeywords.Contains(keyWord));
                return db.Select(q).Count();
            }
        }

        public bool DeleteShop(int id)
        {
            bool result = false;
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    db.Delete<ShopEntity>(x => x.Id == id);
                }
                result = true;
            }
            catch
            {

            }
            return result;
        }
    }
}
