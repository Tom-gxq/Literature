using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class CashApplyRepository : EfRepository<CashApplyEntity>
    {
        public CashApplyRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool Add(CashApplyEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }

        public List<CashApplyEntity> GetAllApply(string accountId)
        {
            return this.Select(x=>x.Status == 0 && x.AccountId == accountId)
                .OrderByDescending(x=>x.CreateTime).ToList();
        }

        public List<CashApplyEntity> GetTradeList(string accountId, DateTime start, DateTime end)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<CashApplyEntity>();
                q = q.Where(a => a.AccountId == accountId && a.CreateTime >= start
                && a.CreateTime < end && a.Status == 1).OrderBy(x => x.CreateTime);
                return db.Select<CashApplyEntity>(q);
            }
        }

        public DateTime? GetMinDate(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<CashApplyEntity>();
                q = q.Where(a => a.AccountId == accountId && a.Status == 1);
                return db.Select<CashApplyEntity>(q).Min(x=>x.CreateTime);
            }
        }
    }
}
