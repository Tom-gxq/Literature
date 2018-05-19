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
    public class ProductTypeRespository : RepositoryBase<ProductTypeEntity,int>
    {
        public ProductTypeRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<ProductTypeEntity> GetProductTypeList(int kind,int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductTypeEntity>().OrderBy(x=>x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public List<ProductTypeEntity> SearchProductTypeByName(string typeName,int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductTypeEntity>().Where(x =>x.TypeName.Contains(typeName)).OrderBy(x => x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public int GetProductTypeListCount()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductTypeEntity>();
                return db.Select(q).Count();
            }
        }

        public int SearchProductTypeByNameCount(string typeName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductTypeEntity>().Where(x =>x.TypeName.Contains(typeName));
                return db.Select(q).Count();
            }
        }

        public List<ProductTypeEntity> GetAllProductTypeList(int kind)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductTypeEntity>().Where(x=>x.Kind == kind).OrderBy(x => x.DisplaySequence);
                return db.Select(q);
            }
        }
    }
}
