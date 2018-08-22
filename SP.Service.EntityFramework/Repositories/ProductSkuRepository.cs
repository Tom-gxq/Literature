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
        public ProductSkuEntity GetProductSkuByProductId(int shopId, string productId,string accountId)
        {
            var result = this.Single(x =>x.ShopId == shopId && x.ProductId == productId && x.AccountId== accountId
            && x.EffectiveTime >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
            return result;
        }
        public int UpdateProductSkuStock(ProductSkuEntity entity)
        {
            return this.UpdateNonDefaults(entity,x=>x.SkuId == entity.SkuId && x.Stock > 0);
        }
        public int RedoProductSkuStock(ProductSkuEntity entity)
        {
            return this.UpdateNonDefaults(entity, x => x.SkuId == entity.SkuId );
        }
        public int UpdateProductSkuOrderNum(ProductSkuEntity entity)
        {
            return this.UpdateNonDefaults(entity, x => x.SkuId == entity.SkuId );
        }

    }
}
