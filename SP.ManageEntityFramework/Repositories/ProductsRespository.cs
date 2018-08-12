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
    public class ProductsRespository : RepositoryBase<ProductEntity, int>
    {
        public ProductsRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<ProductEntity> GetProductList(int saleStatus, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>().Where(x => x.SaleStatus >= saleStatus).OrderByDescending(x => x.UpdateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select<ProductEntity>(q);
            }
        }
        public List<ProductFullEntity> GetShopProductList(int shopId, int saleStatus, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                if(saleStatus>=0)
                {
                    q = q.Where(x => x.SaleStatus == saleStatus);
                }
                q = q.Join<ShopProductEntity>((a, e) => a.ProductId == e.ProductId && e.ShopId == shopId);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select<ProductFullEntity>(q);
            }
        }
        public int GetProductListCount(int saleStatus)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>().Where(x => x.SaleStatus >= saleStatus);
                return db.Select(q).Count();
            }
        }
        public int GetShopProductListCount(int shopId, int saleStatus)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>().Where(x => x.SaleStatus >= saleStatus);
                q = q.LeftJoin<ShopProductEntity>((a, e) => a.ProductId == e.ProductId && e.ShopId == shopId);
                return db.Select(q).Count();
            }
        }

        public List<ProductEntity> SearchProductList(string keyWord,int typeId,int brandId, int saleStatus,int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                if(brandId > 0)
                {
                    q = q.Join<ProductEntity,BrandEntity>((e,a) =>a.Id== brandId && a.Id == e.BrandId);
                }
                if(typeId >0)
                {
                    q = q.Join<ProductEntity,ProductTypeEntity>((e, a) => a.Id == typeId && a.Id == e.TypeId);
                }
                if(!string.IsNullOrEmpty(keyWord))
                {
                    q = q.Where(x => x.Meta_Keywords.Contains(keyWord) );
                }
                if (saleStatus >= 0)
                {
                    q = q.Where(x => x.SaleStatus == saleStatus);
                }
                else
                {
                    q = q.Where(x => x.SaleStatus >= saleStatus);
                }
                q =q.OrderByDescending(x => x.UpdateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public int SearchProductListCount(string keyWord, int typeId, int brandId, int saleStatus)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                if (brandId > 0)
                {
                    q = q.Join<ProductEntity, BrandEntity>((e, a) => a.Id == brandId && a.Id == e.BrandId);
                }
                if (typeId > 0)
                {
                    q = q.Join<ProductEntity, ProductTypeEntity>((e, a) => a.Id == typeId && a.Id == e.TypeId);
                }
                if (!string.IsNullOrEmpty(keyWord))
                {
                    q = q.Where(x => x.Meta_Keywords.Contains(keyWord) );
                }
                if (saleStatus >= 0)
                {
                    q = q.Where(x => x.SaleStatus == saleStatus);
                }
                else
                {
                    q = q.Where(x => x.SaleStatus >= saleStatus);
                }
                return db.Select(q).Count();
            }
        }

        public BrandEntity GetProductBrandByProductId(string productId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<BrandEntity>();
                q = q.Join<BrandEntity, ProductEntity>((a, e) => a.Id == e.BrandId);
                return db.Single<BrandEntity>(q);
            }
        }

        public ProductTypeEntity GetProductProductTypeByProductId(string productId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductTypeEntity>();
                q = q.Join<ProductTypeEntity, ProductEntity>((a, e) => a.Id == e.BrandId);
                return db.Single<ProductTypeEntity>(q);
            }
        }

        public List<AttributeEntity> GetProductAttributeByProductId(string productId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AttributeEntity>();
                q = q.Join<AttributeEntity, ProductAttributeEntity>((a, e) => a.Id == e.AttributeId && e.ProductId == productId);
                return db.Select<AttributeEntity>(q);
            }
        }
        public List<ProductImageEntity> GetImageListByProductId(string productId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductImageEntity>().Where(x=>x.ProductId == productId)
                    .OrderBy(x=>x.DisplaySequence).OrderBy(x=>x.CreateTime);
                return db.Select<ProductImageEntity>(q);
            }
        }

        public List<ProductFullEntity> GetProductListByOrderId(string orderId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q=q.Join<ProductEntity, ShoppingCartsEntity>((x,e)=>x.ProductId == e.ProductId && e.OrderId == orderId);
                return db.Select<ProductFullEntity>(q);
            }
        }

        public List<ProductEntity> SearchProductByKeyWord(string keyWord, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                
                if (!string.IsNullOrEmpty(keyWord))
                {
                    q = q.Where(x => x.Meta_Keywords.Contains(keyWord));
                }
                q = q.OrderByDescending(x => x.UpdateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public List<ProductEntity> SearchTypeProductByKeyWord(string keyWord, int typeId, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>();

                if (!string.IsNullOrEmpty(keyWord))
                {
                    q = q.Where(x =>x.TypeId == typeId && x.Meta_Keywords.Contains(keyWord));
                }
                q = q.OrderByDescending(x => x.UpdateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public List<ProductEntity> GetSellerProductListByTypeId(string accountId, int typeId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(x=>x.SuppliersId == accountId && x.SecondTypeId == typeId);
                return db.Select(q);
            }
        }
    }
}
