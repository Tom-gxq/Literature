using Castle.MicroKernel.Registration;
using Lib.EntityFramework.EntityFramework.Dependency;
using Lib.EntityFramework.EntityFramework.Repositories;
using Lib.EntityFramework.EntityFramework.Uow;
using LibMain.Collections.Extensions;
using LibMain.Logging;
using LibMain.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework
{
    public class LibEntityFrameworkModule:LibMain.Modules.Module
    {
        public ILogger Logger { get; set; }

        private readonly ITypeFinder _typeFinder;

        public LibEntityFrameworkModule(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
            Logger = NullLogger.Instance;
        }

        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new EntityFrameworkConventionalRegisterer());
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            IocManager.IocContainer.Register(
                Component.For(typeof(IDbContextProvider<>))
                    .ImplementedBy(typeof(UnitOfWorkDbContextProvider<>))
                    .LifestyleTransient()
                );

            RegisterGenericRepositories();
        }

        private void RegisterGenericRepositories()
        {
            var dbContextTypes =
                _typeFinder.Find(type =>
                    type.IsPublic &&
                    !type.IsAbstract &&
                    type.IsClass &&
                    typeof(LibDbContext).IsAssignableFrom(type)
                    );

            if (dbContextTypes.IsNullOrEmpty())
            {
                Logger.Warn("No class found derived from AbpDbContext.");
                return;
            }

            foreach (var dbContextType in dbContextTypes)
            {
                EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, IocManager);
            }
        }
    }
}
