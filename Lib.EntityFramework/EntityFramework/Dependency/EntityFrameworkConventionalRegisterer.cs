using Castle.MicroKernel.Registration;
using LibMain.Configuration.Startup;
using LibMain.Dependency;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework.Dependency
{
    /// <summary>
    /// Registers classes derived from AbpDbContext with configurations.
    /// </summary>
    public class EntityFrameworkConventionalRegisterer : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly)
                    .IncludeNonPublicTypes()
                    .BasedOn<LibDbContext>()
                    .WithServiceSelf()
                    .LifestyleTransient()
                    .Configure(c => c.DynamicParameters(
                        (kernel, dynamicParams) =>
                        {
                            var connectionString = GetNameOrConnectionStringOrNull(context.IocManager);
                            if (!string.IsNullOrWhiteSpace(connectionString))
                            {
                                dynamicParams["nameOrConnectionString"] = connectionString;
                            }
                        })));
        }

        private static string GetNameOrConnectionStringOrNull(IIocResolver iocResolver)
        {
            if (iocResolver.IsRegistered<IStartupConfiguration>())
            {
                var defaultConnectionString = iocResolver.Resolve<IStartupConfiguration>().DefaultNameOrConnectionString;
                if (!string.IsNullOrWhiteSpace(defaultConnectionString))
                {
                    return defaultConnectionString;
                }
            }

            if (ConfigurationManager.ConnectionStrings.Count == 1)
            {
                return ConfigurationManager.ConnectionStrings[0].Name;
            }

            if (ConfigurationManager.ConnectionStrings["Default"] != null)
            {
                return "Default";
            }

            return null;
        }
    }
}
