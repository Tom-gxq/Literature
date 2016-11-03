using Autofac;
using Autofac.Core;
using Autofac.Extras.DynamicProxy2;
using LibMain.Dependency;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public class LibMainContainer : IContainer
    {
        private Autofac.IContainer container;        
        public LibMainContainer()
        {
            var buider = new ContainerBuilder();
            this.container = buider.Build();
        }

        public IContainer Install(params IInstaller[] installers)
        {
            foreach(var item in installers)
            {
                item.Install(this);
            }
            return this;
        }

        public bool IsRegistered(Type type)
        {
            if(this.container != null)
            {
                return this.container.IsRegistered(type);
            }
            else
            {
                return false;
            }
        }

        public bool IsRegistered<TType>()
        {
            if (this.container != null)
            {
                return this.container.IsRegistered<TType>();
            }
            else
            {
                return false;
            }
        }

        public IContainer Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class
        {
            var buider = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterType<T>().InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterType<T>().SingleInstance();
                    break;
            }
            buider.Update(this.container);
            return this;
        }

        public IContainer RegisterIntecptor<T1,T2>()
        {
            var buider = new ContainerBuilder();
            buider.RegisterType(typeof(T1)).EnableInterfaceInterceptors().InterceptedBy(typeof(T2));
            buider.RegisterType(typeof(T1));
            buider.RegisterType(typeof(T2));
            buider.Update(this.container);
            return this;
        }

        public IContainer RegisterAssemblyTypes<T>(Assembly assemblies, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            var buider = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterAssemblyTypes(assemblies).As<T>().InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterAssemblyTypes(assemblies).As<T>().SingleInstance();
                    break;
            }
            buider.Update(this.container);
            return this;
        }

        public IContainer Register(Type type ,DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            var buider = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterType(type).InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterType(type).SingleInstance();
                    break;
            }
            buider.Update(this.container);
            return this;
        }

        public IContainer Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            var buider = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterType<TImpl>().As<TType>().InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterType<TImpl>().As<TType>().SingleInstance();
                    break;
            }
            buider.Update(this.container);
            var obj = this.container.Resolve<TType>();
            this.container.InjectUnsetProperties(obj);
            return this;
        }
        
        public IContainer RegisterInstance<TType, TImpl>(TImpl instance, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TImpl : class, TType
        {
            var buider = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterInstance<TImpl>(instance).As<TType>().InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterInstance<TImpl>(instance).As<TType>().SingleInstance();
                    break;
            }
            buider.Update(this.container);
            var obj = this.container.Resolve<TType>();
            this.container.InjectUnsetProperties(obj);
            return this;
        }

        public IContainer Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            var buider = new ContainerBuilder();
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterType(impl).InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterType(impl).SingleInstance();
                    break;
            }
            buider.Update(this.container);
            return this;
        }

        public void Release(object instance)
        {
            container.InjectUnsetProperties(instance);
        }

        public T Resolve<T>()
        {
            try
            {
                return container.Resolve<T>();
            }
            catch(Exception ex)
            {
                return default(T);
            }
        }

        public T Resolve<T>(string key)
        {
            return container.ResolveKeyed<T>(key);
        }
        public object Resolve(Type service)
        {
            try
            {
                return container.Resolve(service);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
        public object Resolve(string key, Type service)
        {
            return container.ResolveKeyed(key, service);
        }
        public object Resolve(Type service, IDictionary arguments)
        {
            if (arguments.Keys != null)
            {
                var parameters = new List<Parameter>();
                foreach (var item in arguments.Keys)
                {
                    parameters.Add(new NamedParameter(item.ToString(), arguments[item]));
                }
                return container.ResolveOptional(service, parameters);
            }
            else
            {
                return null;
            }
        }

        public object Resolve(string key, Type service, IDictionary arguments)
        {
            if (arguments.Keys != null)
            {
                var parameters = new List<Parameter>();
                foreach (var item in arguments.Keys)
                {
                    parameters.Add(new NamedParameter(item.ToString(), arguments[item]));
                }
                return container.ResolveKeyed(key,service, parameters);
            }
            else
            {
                return null;
            }
        }
        public void Dispose()
        {
            container.Dispose();
            container = null;
        }
    }
}
