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
    public class ProductSkuRespository : RepositoryBase<ProductSkuEntity, string>
    {
        public ProductSkuRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<ProductSkuFullEntity> GetProductSkuList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductSkuEntity>();
                q= q.Join<ProductSkuEntity, ProductEntity>((a,b)=>a.ProductId == b.ProductId).OrderByDescending(x=>x.EffectiveTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select<ProductSkuFullEntity>(q);
            }
        }
        public long GetProductSkuListCount()
        {
            return this.Count();
        }
        public bool AddProductSku(ProductSkuEntity resEvent)
        {
            var result = this.Insert(resEvent);
            return result > 0;
        }
        public bool DeleteProductSku(string skuId)
        {
            var result = this.Delete(x => x.Id == skuId);
            return result > 0;
        }
    }
}
