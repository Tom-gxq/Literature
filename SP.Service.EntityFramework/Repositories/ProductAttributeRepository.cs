using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ProductAttributeRepository:EfRepository<ProductAttributeEntity>
    {
        public ProductAttributeRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public List<ProductAttributeInfoEntity> GetAttributeByProductId(string productId)
        {
            var results = new List<ProductAttributeInfoEntity>();
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductAttributeEntity>();
                q = q.Join<ProductAttributeEntity, AttributeEntity>((a, e) => a.ProductId == productId && a.AttributeId == e.Id );
                q = q.Join<ProductAttributeEntity, AttributeValueEntity>((b, f) => b.ValueId == f.Id);
                return db.Select<ProductAttributeInfoEntity>(q);
            }
        }
    }
}
