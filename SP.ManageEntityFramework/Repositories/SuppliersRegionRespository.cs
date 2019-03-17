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
                q = q.Limit((pageIndex - 1) * pageSize, pageSize)
                    .OrderByDescending(x => x.CreateTime);
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

        public bool DelRegion(int id)
        {
            var result = this.DeleteById(id);
            return result > 0;
        }

        public List<SuppliersRegionEntity> SearchRegionByName(string supplierName, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersRegionEntity>();
                q = q.Join<SuppliersRegionEntity, SuppliersEntity>((x,y)=> y.SuppliersName.Contains(supplierName) && x.SuppliersId == y.Id);
                q = q.OrderByDescending(x => x.CreateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize)
                    .OrderByDescending(x => x.CreateTime);
                return db.Select<SuppliersRegionEntity>(q);
            }
        }

        public int SearchRegionByNameCount(string supplierName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersRegionEntity>();
                q = q.Join<SuppliersRegionEntity, SuppliersEntity>((x, y) => y.SuppliersName.Contains(supplierName) && x.SuppliersId == y.Id);
                return db.Select<SuppliersRegionEntity>(q).Count;
            }
        }
    }
}
