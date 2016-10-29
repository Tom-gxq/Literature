using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Configuration.Startup
{
    public interface IModuleConfigurations
    {
        /// <summary>
        /// Gets the ABP configuration object.
        /// </summary>
        IStartupConfiguration Configuration { get; }
    }
}
