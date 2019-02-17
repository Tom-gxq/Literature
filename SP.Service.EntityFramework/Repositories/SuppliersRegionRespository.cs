using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SuppliersRegionRespository : EfRepository<SuppliersRegionEntity>
    {
        public SuppliersRegionRespository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddSuppliersRegion(SuppliersRegionEntity suppliers)
        {
            var result = this.Insert(suppliers);
            return result > 0;
        }

        public List<SuppliersProductFullEntity> GetSuppliersProduct(int supplierId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<SuppliersProductEntity>();
                q = q.Join<SuppliersProductEntity, ProductEntity>((e, a) => e.SuppliersId == supplierId 
                && e.Status == 0 && e.ProductId == a.ProductId);                
                return db.Select<SuppliersProductFullEntity>(q);
            }
        }
    }
}
