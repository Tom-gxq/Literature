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
    public class BrandRespository : RepositoryBase<BrandEntity, int>
    {
        public BrandRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<BrandEntity> GetBrandList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<BrandEntity>().OrderBy(x => x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public List<BrandEntity> GetAllBrandList()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<BrandEntity>().OrderBy(x => x.DisplaySequence);
                return db.Select(q);
            }
        }

        public List<BrandEntity> SearchBrandByName(string brandName, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<BrandEntity>().Where(x => x.BrandName.Contains(brandName)).OrderBy(x => x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public int SearchBrandByNameCount(string brandName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<BrandEntity>().Where(x => x.BrandName.Contains(brandName));
                return db.Select(q).Count();
            }
        }

        public bool DeleteBrand(int brandId)
        {
            bool result = false;
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    db.Delete<BrandEntity>(x => x.Id == brandId);
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
