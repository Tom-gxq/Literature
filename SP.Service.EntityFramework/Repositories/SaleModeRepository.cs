using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SaleModeRepository : EfRepository<SaleModeEntity>
    {
        public SaleModeRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool Add(SaleModeEntity entity)
        {
            var result = this.Insert(entity);
            return result > 0;
        }
        public List<SaleModeEntity> GetSaleModeList(int type)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<SaleModeEntity>();
                q = q.Where(x => x.Type == type).OrderBy(x=>x.ModelAmount);
                return db.Select<SaleModeEntity>(q);
            }
        }
    }
}
