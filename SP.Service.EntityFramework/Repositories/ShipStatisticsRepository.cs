using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ShipStatisticsRepository : EfRepository<ShipStatisticsEntity>
    {
        public ShipStatisticsRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public ShipStatisticsEntity GetTodayOrderStatistic(string accountId, int dormId, DateTime createTime)
        {
            var result = this.Single(x => x.DormId == dormId && x.AccountId == accountId && x.CreateTime == createTime);
            return result;
        }

        public bool AddOrderStatistic(ShipStatisticsEntity shipStatistic)
        {
            var result = this.Insert(shipStatistic);
            return result > 0;
        }
        public bool UpdateOrderStatistic(string accountId,int dormId,DateTime createTime,double foodAmount,double markAmount)
        {
            using (var db = OpenDbConnection())
            {
                var result = db.ExecuteSql($"UPDATE SP_ShipStatistics SET Num_NewOrder = Num_NewOrder + 1," +
                    $" Num_FoodOrderAmount = isnull(Num_FoodOrderAmount,0) + @foodAmount, " +
                    $" Num_MarkOrderAmount = isnull(Num_MarkOrderAmount,0) + @markAmount  " +
                    $" WHERE AccountId = @accountId AND " +
                    $" DormId = @dormId AND CreateTime = @createTime", new { foodAmount, markAmount,accountId, dormId , createTime });
                return result > 0;
            }
        }

        public bool UpdateOrderStatisticCheckedStatus(string accountId, int dormId, DateTime createTime,bool isChecked)
        {
            var result = this.UpdateNonDefaults(new ShipStatisticsEntity()
            {
                IsChecked = isChecked,
                UpdateTime = DateTime.Now
            }, x => x.AccountId == accountId && x.DormId == dormId && x.CreateTime == createTime);
            return result > 0;
        }
    }
}
