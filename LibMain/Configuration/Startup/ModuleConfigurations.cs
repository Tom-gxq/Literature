using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Configuration.Startup
{
    internal class ModuleConfigurations: IModuleConfigurations
    {
        public IStartupConfiguration Configuration { get; private set; }

        public ModuleConfigurations(IStartupConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
