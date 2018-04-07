using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ProductRepository :EfRepository<ProductEntity>
    {
        public ProductRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public ProductEntity GetProductById(string productId)
        {
            var result = this.Single(x => x.ProductId == productId);
            return result;
        }
        
        public List<ProductEntity> GetProductList(int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(a => a.SaleStatus == 1);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }

        public List<ProductEntity> GetProductListByBrandId(int brandId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(a => a.SaleStatus == 1 && a.BrandId== brandId);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }

        public List<ProductEntity> GetProductListByTypeId(long typeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(a => a.SaleStatus == 1 && a.TypeId== typeId);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }

        public List<ProductAttributeEntity> GetProductListByAttributeId(long attributeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductAttributeEntity>();
                q = q.Join<ProductAttributeEntity, ProductEntity>((a, e) => a.AttributeId == attributeId && a.ProductId == e.ProductId && e.SaleStatus==1);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }

        public List<ProductFullEntity> GetShopProductList(int districtId, int shopId,long attributeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Join<ProductEntity, ProductAttributeEntity>((e, a) => a.AttributeId == attributeId && a.ProductId == e.ProductId && e.SaleStatus == 1);
                q = q.Join<ProductEntity, ShopProductEntity>((e, a) => a.ShopId == shopId && a.ProductId == e.ProductId);
                q = q.LeftJoin<ShopProductEntity, ProductSkuEntity>((e, a) => a.ProductId == e.ProductId && e.ShopId==a.ShopId 
                && a.EffectiveTime >= DateTime.Parse(DateTime.Now.ToShortDateString()));
                //q = q.Join<ProductEntity, ProductRegionEntity>((e, a) => a.ProductId == e.ProductId && a.DataId == districtId);
                q = q.OrderByDescending<ProductSkuEntity>(a=>a.Stock).Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select<ProductFullEntity>(q);
            }
        }
        public int GetShopProductCount(int districtId, int shopId, long attributeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Join<ProductEntity,ProductAttributeEntity>((e, a) => a.AttributeId == attributeId && a.ProductId == e.ProductId && e.SaleStatus == 1);
                q = q.Join<ProductEntity,ShopProductEntity>((e, a) => a.ShopId == shopId && a.ProductId == e.ProductId);
                //q = q.Join<ProductEntity, ProductRegionEntity>((e, a) => a.ProductId == e.ProductId && a.DataId == districtId);
                return db.Select(q).Count();
            }
        }

        public List<ProductEntity> SearchProductKeywordList(string keyword, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(a => a.SaleStatus == 1 && a.Meta_Keywords.Contains(keyword));
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }

        public List<CarouselEntity> GetCarouselList()
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<CarouselEntity>();
                return db.Select(q);
            }
        }
    }
}
