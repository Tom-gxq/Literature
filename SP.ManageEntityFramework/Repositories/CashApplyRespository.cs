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
    public class CashApplyRespository : RepositoryBase<CashApplyEntity, int>
    {
        public CashApplyRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<CashApplyEntity> GetCashApplyList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<CashApplyEntity>().OrderBy(x=>x.Status).OrderByDescending(x => x.CreateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public List<CashApplyEntity> SearchCashApplyByDate(int status,DateTime startDate, DateTime endDate)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<CashApplyEntity>().Where(x=>x.Status == status && x.CreateTime >= startDate && x.CreateTime <= endDate);
                q = q.OrderBy(x => x.Status).OrderByDescending(x => x.CreateTime);
                return db.Select(q);
            }
        }
        public long GetCashApplyListCount()
        {
            return this.Count();
        }
        public List<CashApplyEntity> GetCashApplyList(string accountId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<CashApplyEntity>().Where(x=>x.AccountId.ToLower() == accountId.ToLower())
                    .OrderByDescending(x => x.CreateTime);
                return db.Select(q);
            }
        }
        public CashApplyEntity GetCashApplyById(int id)
        {
            return this.Single(x=>x.Id == id);
        }
    }
}
