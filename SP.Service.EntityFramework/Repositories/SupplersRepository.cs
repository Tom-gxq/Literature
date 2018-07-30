using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SupplersRepository : EfRepository<SuppliersEntity>
    {
        public SupplersRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public SuppliersEntity GetSupplerById(int suppliersId)
        {
            var result = this.Single(x => x.Id == suppliersId);
            return result;
        }
        public bool AddSuppler(SuppliersEntity entity)
        {
            var result = this.Insert(entity);
            return result > 0;
        }
        public bool UpdateSupplerStatus(SuppliersEntity entity)
        {
            var result = this.UpdateNonDefaults(entity, x => x.Id == entity.Id);
            return result > 0;
        }

        public List<SuppliersEntity> GeSupplerList(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<SuppliersEntity>().Where(x=>x.Status == 0)
                    .OrderByDescending(x=>x.CreateTime);
                return db.Select(q);
            }
        }
    }
}
