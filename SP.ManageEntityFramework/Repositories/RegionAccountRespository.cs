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
    public class RegionAccountRespository : RepositoryBase<RegionAccountEntity, string>
    {
        public RegionAccountRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public bool Add(RegionAccountEntity data)
        {
            var result = this.Insert(data);
            return result > 0;
        }

        public bool DeleteLeader(RegionAccountEntity data)
        {
            var result = this.Delete(data);
            return result > 0;
        }

        public List<RegionAccountEntity> GetRegionAccount( int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionAccountEntity>().Where(x => x.Status == 0);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public List<RegionAccountEntity> SearchLeaderByName(string leaderName, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionAccountEntity>();
                q = q.Join<RegionAccountEntity, AccountInfoEntity>((x, y) => x.Id == y.AccountId && y.Fullname.Contains(leaderName));
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select<RegionAccountEntity>();
            }
        }

        public int SearchLeaderByNameCount(string leaderName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<RegionAccountEntity>();
                q = q.Join<RegionAccountEntity, AccountInfoEntity>((x, y) => x.Id == y.AccountId && y.Fullname.Contains(leaderName));
                return db.Select<RegionAccountEntity>().Count;
            }
        }
    }
}
