using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ShopRespository : EfRepository<ShopEntity>
    {
        public ShopRespository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public List<ShopEntity> GetShopList(int regionId, int shopType, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ShopEntity>().Where(x=>x.RegionId == regionId && x.ShopType == shopType
                && (string.Compare(x.EndTime, DateTime.Now.ToString("HH:mm")) >= 0 || x.EndTime == null || x.EndTime.Trim() == string.Empty))
                .OrderBy(x => x.DisplaySequence);
                q = q.Skip((pageIndex - 1) * pageSize).Limit(pageSize);
                return db.Select(q);
            }
        }

        public int GetAllShopCount(int regionId, int shopType)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ShopEntity>().Where(x=>x.RegionId == regionId && x.ShopType == shopType 
                && (string.Compare(x.EndTime , DateTime.Now.ToString("HH:mm"))>=0 ||x.EndTime==null ||x.EndTime.Trim() == string.Empty) );
                return db.Select(q).Count();
            }
        }

        public ShopEntity GetShopById(int shopId)
        {
            using (var db = OpenDbConnection())
            {
                return db.Single<ShopEntity>(x=>x.Id == shopId);
            }
        }

        
        
    }
}
