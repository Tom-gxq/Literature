using LibMain.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework.Uow
{
    /// <summary>
    /// Implements <see cref="IDbContextProvider{TDbContext}"/> that gets DbContext from
    /// active unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
    public class UnitOfWorkDbContextProvider<TDbContext> : IDbContextProvider<TDbContext>
        where TDbContext : LibDbContext
    {
        /// <summary>
        /// Gets the DbContext.
        /// </summary>
        public TDbContext DbContext { get { return _currentUnitOfWorkProvider.Current.GetDbContext<TDbContext>(); } }

        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        /// <summary>
        /// Creates a new <see cref="UnitOfWorkDbContextProvider{TDbContext}"/>.
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public UnitOfWorkDbContextProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
        }
    }
}
