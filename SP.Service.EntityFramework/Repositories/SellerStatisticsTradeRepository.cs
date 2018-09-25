using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SellerStatisticsTradeRepository : EfRepository<SellerStatisticsTradeEntity>
    {
        public SellerStatisticsTradeRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddStatisticTrade(SellerStatisticsTradeEntity statistic)
        {
            var result = this.Insert(statistic);
            return result > 0;
        }
    }
}
