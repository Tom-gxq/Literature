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
    public class SellerStatisticsRespository : RepositoryBase<SellerStatisticsEntity, int>
    {
        public SellerStatisticsRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public SellerStatisticsEntity GetSellerStatistics(int sellerId, DateTime dateTime)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<SellerStatisticsEntity>();
                q = q.Join<SellerStatisticsEntity, SuppliersEntity>((a, e) => a.AccountId == e.AccountId && a.CreateTime == dateTime
                 && e.Id == sellerId );
                return db.Single(q);
            }
        }
    }
}
