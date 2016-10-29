using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Dependency
{
    /// <summary>
    /// This class is used to register basic dependency implementations such as <see cref="ITransientDependency"/> and <see cref="ISingletonDependency"/>.
    /// </summary>
    public class BasicConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            //Transient
            context.IocManager.IocContainer.RegisterAssemblyTypes<ITransientDependency>(context.Assembly, DependencyLifeStyle.Transient);

            //Singleton
            context.IocManager.IocContainer.RegisterAssemblyTypes<ITransientDependency>(context.Assembly, DependencyLifeStyle.Singleton);

            //Windsor Interceptors
            //context.IocManager.IocContainer.Register(
            //    Classes.FromAssembly(context.Assembly)
            //        .IncludeNonPublicTypes()
            //        .BasedOn<IInterceptor>()
            //        .WithService.Self()
            //        .LifestyleTransient()
            //    );
        }
    }
}
