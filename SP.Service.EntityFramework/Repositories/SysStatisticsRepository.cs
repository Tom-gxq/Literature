using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SysStatisticsRepository : EfRepository<SysStatisticsEntity>
    {
        public SysStatisticsRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public SysStatisticsEntity GetTodaySysStatistic(DateTime createTime)
        {
            var result = this.Single(x => x.CreateTime == createTime);
            return result;
        }

        public bool AddSysStatistic(SysStatisticsEntity sysStatistic)
        {
            var result = this.Insert(sysStatistic);
            return result > 0;
        }
        public bool UpdateOrderStatistic(DateTime createTime, double foodAmount,double markAmount)
        {
            using (var db = OpenDbConnection())
            {
                var result = db.ExecuteSql($"UPDATE SP_SysStatistics SET Num_NewOrder = Num_NewOrder + 1," +
                    $" Num_FoodOrderAmount = isnull(Num_FoodOrderAmount,0) + @foodAmount,  " +
                    $" Num_MarkOrderAmount = isnull(Num_MarkOrderAmount,0) + @markAmount  " +
                    $" WHERE  CreateTime = @createTime", new { foodAmount, markAmount, createTime });
                return result > 0;
            }
        }
        public bool UpdateNewUserStatistic(DateTime createTime)
        {
            using (var db = OpenDbConnection())
            {
                var result = db.ExecuteSql($"UPDATE SP_SysStatistics SET Num_NewUser = Num_NewUser + 1 " +                    
                    $" WHERE  CreateTime = @createTime", new { createTime });
                return result > 0;
            }
        }
        public bool UpdateNewAssociatorStatistic(DateTime createTime)
        {
            using (var db = OpenDbConnection())
            {
                var result = db.ExecuteSql($"UPDATE SP_SysStatistics SET Num_NewAssociator = Num_NewAssociator + 1 " +
                    $" WHERE  CreateTime = @createTime", new { createTime });
                return result > 0;
            }
        }

        public bool UpdateBuyAssociatorStatistic(DateTime createTime)
        {
            using (var db = OpenDbConnection())
            {
                var result = db.ExecuteSql($"UPDATE SP_SysStatistics SET Num_BuyAssociator = Num_BuyAssociator + 1 " +
                    $" WHERE  CreateTime = @createTime", new { createTime });
                return result > 0;
            }
        }

        public bool UpdateOrderStatisticCheckedStatus(DateTime createTime, bool isChecked)
        {
            var result = this.UpdateNonDefaults(new SysStatisticsEntity()
            {
                IsChecked = isChecked,
                UpdateTime = DateTime.Now
            }, x =>  x.CreateTime == createTime);
            return result > 0;
        }
    }
}
