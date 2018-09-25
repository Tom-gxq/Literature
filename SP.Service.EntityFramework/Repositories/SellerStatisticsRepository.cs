using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SellerStatisticsRepository : EfRepository<SellerStatisticsEntity>
    {
        public SellerStatisticsRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public SellerStatisticsEntity GetTodayStatistic(DateTime createTime, string accountId)
        {
            var result = this.Single(x => x.CreateTime == createTime && x.AccountId == accountId);
            return result;
        }

        public bool AddStatistic(SellerStatisticsEntity sysStatistic)
        {
            var result = this.Insert(sysStatistic);
            return result > 0;
        }
        public bool UpdateOrderStatistic(string SSID, double amount)
        {
            using (var db = OpenDbConnection())
            {
                var result = db.ExecuteSql($"UPDATE SP_SellerStatistics SET Num_NewOrder = Num_NewOrder + 1," +
                    $" Num_OrderAmount = Num_OrderAmount + @amount  " +
                    $" WHERE  SSID = @SSID", new { amount, SSID });
                return result > 0;
            }
        }
        

        public bool UpdateOrderStatisticCheckedStatus(DateTime createTime, bool isChecked)
        {
            var result = this.UpdateNonDefaults(new SellerStatisticsEntity()
            {
                IsChecked = isChecked,
                UpdateTime = DateTime.Now
            }, x => x.CreateTime == createTime);
            return result > 0;
        }
    }
}
