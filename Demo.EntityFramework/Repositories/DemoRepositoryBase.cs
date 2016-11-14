using Lib.EntityFramework.EntityFramework;
using Lib.EntityFramework.EntityFramework.Repositories;
using LibMain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.EntityFramework.Repositories
{
    public abstract class DemoRepositoryBase<TEntity, TPrimaryKey> : ServiceStackRepositoryBase<DemoDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected DemoRepositoryBase(IDbContextProvider<DemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
    public abstract class DemoRepositoryBase<TEntity> : DemoRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected DemoRepositoryBase(IDbContextProvider<DemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
