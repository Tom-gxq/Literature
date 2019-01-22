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

        public bool UpdateLeader(RegionAccountEntity data)
        {
            var result = this.UpdateNonDefaults(data, x => x.Id == data.Id && x.RegionId == data.RegionId);
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
    }
}
