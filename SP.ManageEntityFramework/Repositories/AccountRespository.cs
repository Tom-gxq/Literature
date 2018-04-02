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
    public class AccountRespository:RepositoryBase<AccountInfoEntity, int>
    {
        public AccountRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<AccountInfoEntity> SearchAccount(string keywords)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AccountInfoEntity>();
                q = q.Join<AccountInfoEntity,AccountEntity>((a, e) => a.AccountId == e.AccountId 
                && (a.Fullname.Contains(keywords) || e.MobilePhone.Contains(keywords) || e.Email.Contains(keywords)));
                return db.Select(q);
            }
        }
    }
}
