using Lib.EntityFramework.EntityFramework;
using Lib.EntityFramework.EntityFramework.Repositories;
using LibMain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework.Repositories
{
    public abstract class RepositoryBase<TEntity, TPrimaryKey> : ServiceStackRepositoryBase<ManageDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected RepositoryBase(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
