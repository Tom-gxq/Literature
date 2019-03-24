using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SuppliersProductRepository : EfRepository<SuppliersProductEntity>
    {
        public SuppliersProductRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddSuppliers(SuppliersProductEntity suppliers)
        {
            var result = this.Insert(suppliers);
            return result > 0;
        }
        public int UpdateSuppliers(SuppliersProductEntity product)
        {
            using (var db = OpenDbConnection())
            {
                return this.UpdateNonDefaults(product, x => x.ProductId == product.ProductId && x.SuppliersId == product.SuppliersId);
            }
        }

        public List<SuppliersProductFullEntity> GetSellerProductList(int regionId, int typeId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<SuppliersProductEntity>();
                q = q.Join<SuppliersProductEntity, SuppliersRegionEntity>((e, a) => e.Status == 0 && e.SaleStatus == 1 && e.SuppliersId == a.SuppliersId 
                && a.RegionID == regionId);
                q = q.Join<SuppliersProductEntity, SuppliersEntity>((e, a) => e.SuppliersId == a.Id && a.TypeId == typeId);
                q = q.Join<SuppliersProductEntity, ProductEntity>((e, a) => e.ProductId == a.ProductId);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select<SuppliersProductFullEntity>(q);
            }
        }

        public List<SuppliersProductFullEntity> GetSellerProductListByAccountId(string accounId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<SuppliersProductEntity>();
                q = q.Join<SuppliersProductEntity, SellerProductEntity>((e, a) => e.Status == 0 && e.SaleStatus == 1 
                && e.Id == a.SupplierProductId && a.AccountId == accounId);
                q = q.Join<SuppliersProductEntity, ProductEntity>((e, a) => e.ProductId == a.ProductId);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select<SuppliersProductFullEntity>(q);
            }
        }
        public List<SuppliersProductFullEntity> GetSuppliersProductById(int supplierProductId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<SuppliersProductEntity>();
                q = q.Join<SuppliersProductEntity, ProductEntity>((e, a) => e.Id == supplierProductId && e.ProductId == a.ProductId);
                return db.Select<SuppliersProductFullEntity>(q);
            }
        }
    }
}
