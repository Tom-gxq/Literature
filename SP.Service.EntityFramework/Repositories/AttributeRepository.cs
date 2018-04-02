using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AttributeRepository : EfRepository<AttributeEntity>
    {
        public AttributeRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public List<AttributeEntity> GetTitleAttributeList(int attType)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<AttributeEntity>().Where(x=>x.Type == attType).OrderBy(x => x.DisplaySequence);
                return db.Select(q);
            }
        }
    }
}
