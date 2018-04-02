using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ProductSkuRepository : EfRepository<ProductSkuEntity>
    {
        public ProductSkuRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public ProductSkuEntity GetProductSkuByProductId(string productId)
        {
            var result = this.Single(x => x.ProductId == productId && x.EffectiveTime >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
            return result;
        }
        public int UpdateProductSkuStock(ProductSkuEntity entity)
        {
            return this.UpdateNonDefaults(entity,x=>x.SkuId == entity.SkuId && x.Stock > 0);
        }

    }
}
