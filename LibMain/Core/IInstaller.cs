using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public interface IInstaller: IWindsorInstaller
    {
        void Install(IContainer container);
    }
}
