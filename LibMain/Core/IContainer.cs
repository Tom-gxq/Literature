using LibMain.Dependency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public interface IContainer:System.IDisposable
    {
        IContainer Install(params IInstaller[] installers);
        //
        // 摘要: 
        //     Registers the components with the Castle.Windsor.IWindsorContainer. The instances
        //     of Castle.MicroKernel.Registration.IRegistration are produced by fluent registration
        //     API.  Most common entry points are Castle.MicroKernel.Registration.Component.For<T0>()
        //     method to register a single type or (recommended in most cases) Castle.MicroKernel.Registration.Classes.FromThisAssembly().
        //      Let the Intellisense drive you through the fluent API past those entry points.
        //     For details see the documentation at http://j.mp/WindsorApi
        //
        // 参数: 
        //   registrations:
        //     The component registrations created by Castle.MicroKernel.Registration.Component.For<T0>(),
        //     Castle.MicroKernel.Registration.Classes.FromThisAssembly() or different entry
        //     method to the fluent API.
        //
        // 返回结果: 
        //     The container.
        IContainer Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class;
        IContainer Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        IContainer Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;
        IContainer Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        IContainer RegisterAssemblyTypes<T>(Assembly assembly, DependencyLifeStyle lifeStyle);
        //
        // 摘要: 
        //     Releases a component instance
        //
        // 参数: 
        //   instance:
        void Release(object instance);
        //
        // 摘要: 
        //     Remove a child container
        //
        // 参数: 
        //   childContainer:
        //void RemoveChildContainer(IContainer childContainer);
        //
        // 摘要: 
        //     Returns a component instance by the service
        //
        // 类型参数: 
        //   T:
        //     Service type
        //
        // 返回结果: 
        //     The component instance
        T Resolve<T>();
        //
        // 摘要: 
        //     Returns a component instance by the service
        //
        // 参数: 
        //   arguments:
        //
        // 类型参数: 
        //   T:
        //     Service type
        //
        // 返回结果: 
        //     The component instance
        //T Resolve<T>(IDictionary arguments);
        //
        // 摘要: 
        //     Returns a component instance by the service
        //
        // 参数: 
        //   argumentsAsAnonymousType:
        //
        // 类型参数: 
        //   T:
        //     Service type
        //
        // 返回结果: 
        //     The component instance
        //T Resolve<T>(object argumentsAsAnonymousType);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //     Component's key
        //
        // 类型参数: 
        //   T:
        //     Service type
        //
        // 返回结果: 
        //     The Component instance
        T Resolve<T>(string key);
        //
        // 摘要: 
        //     Returns a component instance by the service
        //
        // 参数: 
        //   service:
        object Resolve(Type service);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //     Component's key
        //
        //   arguments:
        //
        // 类型参数: 
        //   T:
        //     Service type
        //
        // 返回结果: 
        //     The Component instance
        //T Resolve<T>(string key, IDictionary arguments);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //
        //   arguments:
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use Resolve<object>(key, arguments) instead.")]
        //object Resolve(string key, IDictionary arguments);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //     Component's key
        //
        //   argumentsAsAnonymousType:
        //
        // 类型参数: 
        //   T:
        //     Service type
        //
        // 返回结果: 
        //     The Component instance
        //T Resolve<T>(string key, object argumentsAsAnonymousType);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //
        //   argumentsAsAnonymousType:
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //[Obsolete("Use Resolve<object>(key, argumentsAsAnonymousType) instead.")]
        //object Resolve(string key, object argumentsAsAnonymousType);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //
        //   service:
        object Resolve(string key, Type service);
        //
        // 摘要: 
        //     Returns a component instance by the service
        //
        // 参数: 
        //   service:
        //
        //   arguments:
        object Resolve(Type service, IDictionary arguments);
        //
        // 摘要: 
        //     Returns a component instance by the service
        //
        // 参数: 
        //   service:
        //
        //   argumentsAsAnonymousType:
        //object Resolve(Type service, object argumentsAsAnonymousType);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //
        //   service:
        //
        //   arguments:
        object Resolve(string key, Type service, IDictionary arguments);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //
        //   service:
        //
        //   argumentsAsAnonymousType:
        //object Resolve(string key, Type service, object argumentsAsAnonymousType);
        //
        // 摘要: 
        //     Resolve all valid components that match this type.
        //
        // 类型参数: 
        //   T:
        //     The service type
        //T[] ResolveAll<T>();
        //
        // 摘要: 
        //     Resolve all valid components that match this type.  The service type Arguments
        //     to resolve the service
        //
        // 参数: 
        //   arguments:
        //     Arguments to resolve the service
        //
        // 类型参数: 
        //   T:
        //     The service type
        //T[] ResolveAll<T>(IDictionary arguments);
        //
        // 摘要: 
        //     Resolve all valid components that match this type.  The service type Arguments
        //     to resolve the service
        //
        // 参数: 
        //   argumentsAsAnonymousType:
        //     Arguments to resolve the service
        //
        // 类型参数: 
        //   T:
        //     The service type
        //T[] ResolveAll<T>(object argumentsAsAnonymousType);
        //
        // 摘要: 
        //     Resolve all valid components that match this service the service to match
        //
        // 参数: 
        //   service:
        //     the service to match
        //Array ResolveAll(Type service);
        //
        // 摘要: 
        //     Resolve all valid components that match this service the service to match
        //     Arguments to resolve the service
        //
        // 参数: 
        //   service:
        //     the service to match
        //
        //   arguments:
        //     Arguments to resolve the service
        //Array ResolveAll(Type service, IDictionary arguments);
        //
        // 摘要: 
        //     Resolve all valid components that match this service the service to match
        //     Arguments to resolve the service
        //
        // 参数: 
        //   service:
        //     the service to match
        //
        //   argumentsAsAnonymousType:
        //     Arguments to resolve the service
        //Array ResolveAll(Type service, object argumentsAsAnonymousType);

    }
}
