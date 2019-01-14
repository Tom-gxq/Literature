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
    public class SuppliersRegionRespository : RepositoryBase<SuppliersRegionEntity, int>
    {
        public SuppliersRegionRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public bool Add(SuppliersRegionEntity data)
        {
            var result = this.Insert(data);
            return result > 0;
        }

        public List<SuppliersRegionEntity> GetSuppliersRegionList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersRegionEntity>();
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select<SuppliersRegionEntity>(q);
            }
        }

        public SuppliersRegionEntity GetSupplierRegionById(int id)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersRegionEntity>().Where(x => x.Id == id);
                return db.Single(q);
            }
        }

        public bool EditRegion(SuppliersRegionEntity entity)
        {
            var result = this.UpdateNonDefaults(entity, x => x.Id == entity.Id);
            return result > 0;
        }
    }
}
