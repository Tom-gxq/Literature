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
                q= q.Join<ProductSkuEntity, ProductEntity>((a,b)=>a.ProductId == b.ProductId && a.EffectiveTime>=DateTime.Parse(DateTime.Now.ToShortDateString()));
                q = q.LeftJoin<ProductSkuEntity, ShopEntity>((a, b) => a.ShopId == b.Id);
                q = q.LeftJoin<ShopEntity, RegionEntity>((a, b) => a.RegionId == b.Id).OrderBy(x=>x.ShopId).OrderByDescending(x => x.EffectiveTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select<ProductSkuFullEntity>(q);
            }
        }
        public long GetProductSkuListCount()
        {
            return this.Count(a=> a.EffectiveTime >= DateTime.Parse(DateTime.Now.ToShortDateString()));
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
        public ProductSkuEntity GetProductSkuById(string skuId)
        {
            return this.Single(x=>x.Id == skuId);
        }
        public bool UpdateOneProductSku(string skuId,int stock)
        {
            var result = this.UpdateNonDefaults(new ProductSkuEntity()
            {
                Stock = stock
            },x=>x.Id == skuId);
            return result > 0;
        }
        public List<ProductSkuFullEntity> SearchProducSku(int schoolId, int districtId, int shopId, string productId, int skuStatus)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductSkuEntity>();
                if(skuStatus == 0)
                {
                    q = q.Where(x=>x.Stock == 0);
                }
                else if(skuStatus >0)
                {
                    q = q.Where(x => x.Stock > 0);
                }
                if (string.IsNullOrEmpty(productId))
                {
                    q = q.Join<ProductSkuEntity, ProductEntity>((a, b) => a.ProductId == b.ProductId && a.EffectiveTime >= DateTime.Parse(DateTime.Now.ToShortDateString()));
                }
                else
                {
                    q = q.Join<ProductSkuEntity, ProductEntity>((a, b) =>a.ProductId == productId && a.ProductId == b.ProductId && a.EffectiveTime >= DateTime.Parse(DateTime.Now.ToShortDateString()));
                }
                if (shopId == 0)
                {
                    q = q.Join<ProductSkuEntity, ShopEntity>((a, b) => a.ShopId == b.Id);
                }
                else
                {
                    q = q.Join<ProductSkuEntity, ShopEntity>((a, b) => a.ShopId == shopId && a.ShopId == b.Id);
                }
                if (districtId == 0 && schoolId== 0)
                {
                    q = q.Join<ShopEntity, RegionEntity>((a, b) => a.RegionId == b.Id);
                }
                else if(districtId>0)
                {
                    q = q.Join<ShopEntity, RegionEntity>((a, b) => a.RegionId == districtId && a.RegionId == b.Id);
                }
                else
                {
                    q = q.Join<ShopEntity, RegionEntity>((a, b) => b.ParentDataID == schoolId  &&a.RegionId == b.Id);
                }
                q = q.OrderBy(x => x.ShopId);
                return db.Select<ProductSkuFullEntity>(q);
            }
        }
    }
}
