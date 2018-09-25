using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ShopOwnerRespository : EfRepository<ShopOwnerEntity>
    {
        public ShopOwnerRespository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public int UpdateOpenShopStatus(string accountId, bool status)
        {
            using (var db = OpenDbConnection())
            {
                return this.UpdateNonDefaults(new ShopOwnerEntity()
                {
                    ShopStatus = status
                }, x => x.OwnerId == accountId);
            }
        }

        public List<ShopOwnerEntity> GetAllShopOwnerList(int shopId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ShopOwnerEntity>().Where(x => x.ShopId == shopId  && x.ShopStatus == true);
                return db.Select(q);
            }
        }



        public ShopOwnerEntity GetShopStatus(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ShopOwnerEntity>().Where(x => x.OwnerId == accountId);
                return db.Single(q);
            }
        }

    }
}
