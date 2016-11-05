using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using LibMain.Configuration.Startup;
using LibMain.Core;
using LibMain.Domain.UnitOfWork;
using LibMain.Localization;
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
            container.Register(
                Component.For<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>().ImplementedBy<UnitOfWorkDefaultOptions>().LifestyleSingleton(),
                Component.For<ILocalizationConfiguration, LocalizationConfiguration>().ImplementedBy<LocalizationConfiguration>().LifestyleSingleton(),
                Component.For<IModuleConfigurations, ModuleConfigurations>().ImplementedBy<ModuleConfigurations>().LifestyleSingleton(),
                Component.For<IStartupConfiguration, StartupConfiguration>().ImplementedBy<StartupConfiguration>().LifestyleSingleton(),
                Component.For<ITypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<IModuleFinder>().ImplementedBy<DefaultModuleFinder>().LifestyleTransient(),
                Component.For<IModuleManager>().ImplementedBy<ModuleManager>().LifestyleSingleton()
                );
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Install(IocManager.Instance.IocContainer);
        }
    }
}
