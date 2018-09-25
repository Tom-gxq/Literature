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
    public class AssociatorRespository:RepositoryBase<AssociatorEntity, int>
    {
        public AssociatorRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<AssociatorEntity> GetAssociatorList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AssociatorEntity>().OrderByDescending(x => x.Id);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public int GetAssociatorListCount()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AssociatorEntity>();
                return db.Select(q).Count();
            }
        }

        public bool DeleteAssociator(string associatorId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var result = db.Delete<AssociatorEntity>(x => x.AssociatorId == associatorId);
                return result > 0;
            }
        }
        public List<AssociatorEntity> SearchAssociatorByAccountId(string accountId, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AssociatorEntity>().Where(x=>x.AccountId == accountId).OrderByDescending(x => x.Id);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
    }
}
