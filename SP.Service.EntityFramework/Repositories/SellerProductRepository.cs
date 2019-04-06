using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SellerProductRepository : EfRepository<SellerProductEntity>
    {
        public SellerProductRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool Add(SellerProductEntity sellerProuct)
        {
            var result = this.Insert(sellerProuct);
            return result > 0;
        }

        public bool Delete(SellerProductEntity sellerProuct)
        {
            var result = this.Delete(x => x.AccountId == sellerProuct.AccountId && x.SupplierProductId == sellerProuct.SupplierProductId);
            return result > 0;
        }

        public SellerProductEntity GetSellerProduct(string accountId, int supplierProductId)
        {
            return this.Single(x => x.AccountId == accountId && x.SupplierProductId == supplierProductId);
        }
        public long GetSellerProductCount()
        {
            return this.Count();
        }

        public List<SellerProductEntity> GetSellerProductList(int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<SellerProductEntity>();
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select(q);
            }
        }

        public bool Update(SellerProductEntity sellerProuct)
        {
            var result = this.UpdateNonDefaults(sellerProuct, x => x.AccountId == sellerProuct.AccountId && x.SupplierProductId == sellerProuct.SupplierProductId);
            return result > 0;
        }

        public List<SellerProductEntity> GetAllFoodProduct()
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<SellerProductEntity>();
                q = q.Where(x=>x.PreStock > 0);
                return db.Select(q);
            }
        }
    }
}
