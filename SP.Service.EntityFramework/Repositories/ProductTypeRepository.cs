using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ProductTypeRepository : EfRepository<ProductTypeEntity>
    {
        public ProductTypeRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public ProductTypeEntity GetProductTypeById(long typeId)
        {
            var result = this.Single(x => x.Id == typeId);
            return result;
        }
        public List<ProductTypeEntity> GetProductTypeByKind(int kind)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductTypeEntity>().Where(x => x.Kind == kind);

                q = q.OrderBy(x => x.DisplaySequence);
                return db.Select(q);
            }
        }
    }
}
