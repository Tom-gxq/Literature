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

        public List<ProductFullEntity> GetShopProductList(int districtId, int shopId,long typeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Join<ProductEntity, ShopProductEntity>((e, a) => a.ProductId == e.ProductId && e.SaleStatus == 1 
                && (e.SecondTypeId == typeId || e.TypeId == typeId));
                q = q.Join<ShopProductEntity, ShopEntity>((a, b) => a.ShopId == b.Id && b.RegionId == districtId);
                q = q.LeftJoin<ShopProductEntity, ProductSkuEntity>((e, a) => a.ProductId == e.ProductId && e.ShopId==a.ShopId 
                && a.EffectiveTime >= DateTime.Parse(DateTime.Now.ToShortDateString()));
                
                //q = q.Join<ProductEntity, ProductRegionEntity>((e, a) => a.ProductId == e.ProductId && a.DataId == districtId);
                q = q.OrderByDescending<ProductSkuEntity>(a=>a.Stock).Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select<ProductFullEntity>(q);
            }
        }
        public List<ProductFullEntity> GetFoodShopProductList(int districtId, int shopId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Join<ProductEntity, ShopProductEntity>((e, a) => a.ShopId == shopId && a.ProductId == e.ProductId && e.SaleStatus == 1);
                //q = q.LeftJoin<ShopProductEntity, ProductSkuEntity>((e, a) => a.ProductId == e.ProductId && e.ShopId == a.ShopId
                //&& a.EffectiveTime >= DateTime.Parse(DateTime.Now.ToShortDateString()));
                //q = q.Join<ProductEntity, ProductRegionEntity>((e, a) => a.ProductId == e.ProductId && a.DataId == districtId);
                //q = q.OrderByDescending<ProductSkuEntity>(a => a.Stock)
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select<ProductFullEntity>(q);
            }
        }
        public int GetShopProductCount(int districtId, int shopId, long typeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Join<ProductEntity,ShopProductEntity>((e, a) =>  a.ProductId == e.ProductId && e.SaleStatus == 1 
                && (e.SecondTypeId == typeId || e.TypeId == typeId));
                q = q.Join<ShopProductEntity, ShopEntity>((a,b)=>a.ShopId == b.Id && b.RegionId== districtId);
                //q = q.Join<ProductEntity, ProductRegionEntity>((e, a) => a.ProductId == e.ProductId && a.DataId == districtId);
                return db.Select(q).Count();
            }
        }
        public int GetFoodShopProductListCount(int districtId, int shopId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Join<ProductEntity, ShopProductEntity>((e, a) => a.ShopId == shopId && a.ProductId == e.ProductId && e.SaleStatus == 1);
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

        public bool Add(ProductEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }
        public int Update(ProductEntity product)
        {
            using (var db = OpenDbConnection())
            {
                return this.UpdateNonDefaults(product, x => x.ProductId == product.ProductId);
            }
        }

        public List<ProductEntity> GetDistributorMarketProduct(long typeId, long secondTypeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(a => a.SaleStatus == 1 && a.TypeId == typeId && a.SecondTypeId == secondTypeId);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }
        public int GetDistributorMarketProductCount(long typeId, long secondTypeId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(a => a.SaleStatus == 1 && a.TypeId == typeId && a.SecondTypeId == secondTypeId);
                return db.Select(q).Count();
            }
        }

        public List<ProductEntity> GetDistributorProduct(int districtId, long secondTypeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Join<ProductEntity,ShopProductEntity>((a,e) => a.ProductId == e.ProductId && a.SaleStatus == 1);
                q = q.Join<ShopProductEntity,ShopEntity>((a, e) => e.RegionId == districtId && e.Id == a.ShopId && e.ShopType == secondTypeId);
                
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }
        public int GetDistributorProductCount(long districtId, long secondTypeId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Join<ProductEntity, ShopProductEntity>((a, e) => a.ProductId == e.ProductId && a.SaleStatus == 1);
                q = q.Join<ShopProductEntity, ShopEntity>((a, e) => e.RegionId == districtId && e.Id == a.ShopId && e.ShopType == secondTypeId);
                return db.Select(q).Count();
            }
        }


        public List<ProductEntity> GetSellerProduct(string accountId,long typeId, long secondTypeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(a =>  a.TypeId == typeId && a.SecondTypeId == secondTypeId && a.SuppliersId == accountId);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }
        public int GetSellerProductCount(string accountId, long typeId, long secondTypeId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ProductEntity>();
                q = q.Where(a =>  a.TypeId == typeId && a.SecondTypeId == secondTypeId && a.SuppliersId == accountId);
                return db.Select(q).Count();
            }
        }
    }
}
