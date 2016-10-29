using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Modules
{

    internal interface IModuleManager
    {
        void InitializeModules();

        void ShutdownModules();
    }
}
