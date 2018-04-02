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
    public class AdminRespository : RepositoryBase<AdminEntity, long>
    {
        public AdminRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<AdminEntity> GetAdminList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AdminEntity>().Where(x => x.IsDel == false).OrderByDescending(x => x.UpdateTime);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }
        public int GetAdminListCount()
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AdminEntity>().Where(x => x.IsDel == false);
                return db.Select(q).Count();
            }
        }

        public List<AdminEntity> SearchAdminByUserName(string userName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AdminEntity>().Where(x => x.IsDel == false && x.UserName.Contains(userName)).OrderByDescending(x => x.UpdateTime);
                return db.Select(q);
            }
        }

        public int SearchAdminByUserNameCount(string userName)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AdminEntity>().Where(x => x.IsDel == false && x.UserName.Contains(userName));
                return db.Select(q).Count();
            }
        }
        public AdminEntity GetAdminById(long id)
        {
            return this.Single(x => x.Id == id);
        }
    }
}
