using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class IncomeTradeRepository : EfRepository<ComTradeEntity>
    {
        public IncomeTradeRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public bool AddTrade(ComTradeEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }

        public List<ComTradeEntity> GetTradeList(string accountId, DateTime start, DateTime end)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ComTradeEntity>();
                q = q.Where(a => a.AccountId == accountId && a.CreateTime >= start
                && a.CreateTime < end ).OrderBy(x => x.CreateTime);
                return db.Select<ComTradeEntity>(q);
            }
        }
        public long GetTradeListCount(string accountId)
        {
            return this.Count(a => a.AccountId == accountId);
        }
        public DateTime? GetMinDate(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<ComTradeEntity>();
                q = q.Where(a => a.AccountId == accountId );
                return db.Select<ComTradeEntity>(q).Min(x => x.CreateTime);
            }
        }
    }
}
