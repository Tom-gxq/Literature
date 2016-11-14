using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework
{
    public sealed class SimpleDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
    {
        public TDbContext DbContext { get; private set; }

        public SimpleDbContextProvider(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
