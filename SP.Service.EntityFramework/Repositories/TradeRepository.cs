using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class TradeRepository : EfRepository<TradeEntity>
    {
        public TradeRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public bool AddTrade(TradeEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }
        public List<TradeFullEntity> GetTradeList(string accountId, int pageIndex, int pageSize)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<TradeEntity>();
                q = q.Join<TradeEntity, ShoppingCartsEntity>((a,e)=>a.CartId == e.CartId);
                q = q.Where(a => a.AccountId == accountId ).OrderByDescending(x=>x.CreateTime);
                q = q.Limit((pageIndex - 1) * pageSize, (pageIndex - 1) * pageSize + pageSize);
                return db.Select<TradeFullEntity>(q);
            }
        }
        public long GetTradeListCount(string accountId)
        {
            return this.Count(a => a.AccountId == accountId);
        }

        public List<TradeEntity> GetLatelyTrade(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<TradeEntity>();
                q = q.Where(a => a.AccountId == accountId && a.CreateTime >= DateTime.Parse(DateTime.Now.ToShortDateString()).AddDays(-10));
                return db.Select(q);
            }
        }
    }
}
