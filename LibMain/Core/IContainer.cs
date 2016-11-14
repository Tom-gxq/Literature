using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
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
        IKernel Kernel { get; }
        IContainer Install(params IInstaller[] installers);
        bool IsRegistered(Type type);
        bool IsRegistered<TType>();        
        IContainer Register(Type type, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);
        IContainer Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType;
        IContainer Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton);

        IContainer Register(params IRegistration[] registrations);
        //
        // 摘要: 
        //     Releases a component instance
        //
        // 参数: 
        //   instance:
        void Release(object instance);
        
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
        T Resolve<T>(object argumentsAsAnonymousType);
        //
        // 摘要: 
        //     Returns a component instance by the service
        //
        // 参数: 
        //   service:
        object Resolve(Type type);
        
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //
        //   service:
        object Resolve(Type type, object argumentsAsAnonymousType);
        

    }
}
