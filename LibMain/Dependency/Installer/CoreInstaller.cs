using LibMain.Configuration.Startup;
using LibMain.Core;
using LibMain.Domain.UnitOfWork;
using LibMain.Modules;
using LibMain.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Dependency.Installer
{
    public class CoreInstaller:IInstaller
    {
        public void Install(IContainer container)
        {
            container.Register<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>(DependencyLifeStyle.Singleton);
            container.Register<IModuleConfigurations, ModuleConfigurations>(DependencyLifeStyle.Singleton);
            container.Register<IStartupConfiguration, StartupConfiguration>(DependencyLifeStyle.Singleton);
            container.Register<ITypeFinder, TypeFinder>(DependencyLifeStyle.Singleton);
            container.Register<IModuleFinder, DefaultModuleFinder>(DependencyLifeStyle.Singleton);
            container.Register<IModuleManager>(DependencyLifeStyle.Singleton);
        }
    }
}
