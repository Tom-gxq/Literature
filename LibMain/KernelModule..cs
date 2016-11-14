using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMain.Modules;
using LibMain.Dependency;
using System.Reflection;
using LibMain.Domain.UnitOfWork;

namespace LibMain
{
    /// <summary>
    /// Kernel (core) module of the ABP system.
    /// No need to depend on this, it's automatically the first module always.
    /// </summary>
    public sealed class KernelModule : Modules.Module
    {
        public override void PreInitialize()
        {
            base.IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());

            //ValidationInterceptorRegistrar.Initialize(base.IocManager);

            //FeatureInterceptorRegistrar.Initialize(base.IocManager);
            //AuditingInterceptorRegistrar.Initialize(base.IocManager);

            UnitOfWorkRegistrar.Initialize(base.IocManager);

            //AuthorizationInterceptorRegistrar.Initialize(base.IocManager);

            //Configuration.Auditing.Selectors.Add(
            //    new NamedTypeSelector(
            //        "Abp.ApplicationServices",
            //        type => typeof(IApplicationService).IsAssignableFrom(type)
            //        )
            //    );

            //Configuration.Localization.Sources.Add(
            //    new DictionaryBasedLocalizationSource(
            //        AbpConsts.LocalizationSourceName,
            //        new XmlEmbeddedFileLocalizationDictionaryProvider(
            //            Assembly.GetExecutingAssembly(), "Abp.Localization.Sources.AbpXmlSource"
            //            )));

            //Configuration.Settings.Providers.Add<LocalizationSettingProvider>();
            //Configuration.Settings.Providers.Add<EmailSettingProvider>();

            //Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.SoftDelete, true);
            //Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MustHaveTenant, true);
            //Configuration.UnitOfWork.RegisterFilter(AbpDataFilters.MayHaveTenant, true);

            ConfigureCaches();
        }

        public override void Initialize()
        {
            base.Initialize();

            //IocManager.IocContainer.Install(new EventBusInstaller(base.IocManager));

            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(),
                new ConventionalRegistrationConfig
                {
                    InstallInstallers = false
                });
        }

        public override void PostInitialize()
        {
            RegisterMissingComponents();
        }

        private void ConfigureCaches()
        {
            
        }

        private void RegisterMissingComponents()
        {
            
        }
    }
}
