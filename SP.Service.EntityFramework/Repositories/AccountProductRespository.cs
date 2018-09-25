using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AccountProductRespository : EfRepository<AccountProductEntity>
    {
        public AccountProductRespository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddAccountProduct(AccountProductEntity entity)
        {
            var result = this.Insert(entity);
            return result > 0;
        }

        public AccountProductEntity GetAccountProduct(string accountId,string productId,int shopId)
        {
            var result = this.Single(x => x.AccountId == accountId 
            && x.ProductId == productId && x.ShopId == shopId);
            return result;
        }
        public List<AccountProductEntity> GetAllFoodAccountProduct()
        {
            return this.Select(x=>x.Status== 0 && x.PreStock > 0);
        }
    }
}
