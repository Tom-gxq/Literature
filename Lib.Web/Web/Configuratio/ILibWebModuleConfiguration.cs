using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Web.Configuratio
{
    /// <summary>
    /// Used to configure ABP Web module.
    /// </summary>
    public interface ILibWebModuleConfiguration
    {
        /// <summary>
        /// If this is set to true, all exception and details are sent directly to clients on an error.
        /// Default: false (ABP hides exception details from clients except special exceptions.)
        /// </summary>
        bool SendAllExceptionsToClients { get; set; }
    }
}
