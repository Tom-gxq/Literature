using LibMain.Domain.Entities;
using LibMain.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework.Repositories
{
    public class ServiceStackRepositoryBase<TDbContext, TEntity> : ServiceStackRepositoryBase<TDbContext, TEntity, int>, IRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TDbContext : LibDbContext
    {
        public ServiceStackRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

    }
}
