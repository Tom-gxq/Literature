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

        public List<RegionAccountEntity> SearchLeaderByName(string leaderName)
        {
            List<RegionAccountEntity> list = new List<RegionAccountEntity>();

            using (var db = Context.OpenDbConnection())
            {
                var leaderQuery = db.From<AccountInfoEntity>().Where(x => x.Fullname.Contains(leaderName));
                foreach (var leader in db.Select(leaderQuery))
                {
                    var q = db.From<RegionAccountEntity>().Where(x => x.Id == leader.AccountId);
                    list.AddRange(db.Select(q));
                }
            }

            return list;
        }
    }
}
