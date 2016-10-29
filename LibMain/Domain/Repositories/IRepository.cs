using LibMain.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Domain.Repositories
{
    /// <summary>
    /// This interface must be implemented by all repositories to identify them by convention.
    /// Implement generic version instead of this one.
    /// </summary>
    public interface IRepository : ITransientDependency
    {

    }
}
