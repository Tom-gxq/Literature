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
    public class SuppliersProductRepository : RepositoryBase<SuppliersProductEntity, int>
    {
        public SuppliersProductRepository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<SuppliersProductEntity> GetProductList(int pageIndex, int pageSize, int id)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersProductEntity>();
                q = q.Where(n => n.SuppliersId == id)?.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select<SuppliersProductEntity>(q);
            }
        }

        public int GetSuppliersProductCount(int id)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SuppliersProductEntity>();
                q = q.Where(n => n.SuppliersId == id);
                return db.Select(q).Count;
            }
        }

        public bool Add(SuppliersProductEntity data)
        {
            var result = this.Insert(data);
            return result > 0;
        }

        public SuppliersProductEntity GetSellerProductById(int id)
        {
            var result = this.Single(x => x.Id == id);
            return result;
        }

        public bool EditProduct(SuppliersProductEntity entity)
        {
            var result = this.UpdateNonDefaults(entity, x => x.Id == entity.Id);
            return result > 0;
        }
    }
}
