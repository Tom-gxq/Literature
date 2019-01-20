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

        public bool DelRegion(int id)
        {
            var result = this.DeleteById(id);
            return result > 0;
        }

        public List<SuppliersRegionEntity> SearchRegionByName(string supplierName)
        {
            List<SuppliersRegionEntity> list = new List<SuppliersRegionEntity>();

            using (var db = Context.OpenDbConnection())
            {
                var supplierQuery = db.From<SuppliersEntity>().Where(x => x.SuppliersName.Contains(supplierName));
                foreach (var supplier in db.Select(supplierQuery))
                {
                    var q = db.From<SuppliersRegionEntity>().Where(x => x.SuppliersId == supplier.Id).OrderByDescending(x => x.UpdateTime);
                    list.AddRange(db.Select(q));
                }
            }

            return list;
        }

        public int SearchRegionByNameCount(string supplierName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersEntity>().Where(x=>x.SuppliersName.Contains(supplierName));
                return db.Select(q).Count();
            }
        }
    }
}
