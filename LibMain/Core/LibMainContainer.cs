using Autofac;
using Autofac.Core;
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
        private ContainerBuilder buider;

        public LibMainContainer()
        {
            buider = new ContainerBuilder();
            container = buider.Build();
        }

        public IContainer Install(params IInstaller[] installers)
        {
            foreach(var item in installers)
            {
                item.Install(this);
            }
            return this;
        }

        public IContainer Register<T>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton) where T : class
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterType<T>().InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterType<T>().SingleInstance();
                    break;
            }
            container = buider.Build();
            return this;
        }

        public IContainer RegisterAssemblyTypes<T>(Assembly assemblies, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterAssemblyTypes(assemblies).As<T>().InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterAssemblyTypes(assemblies).As<T>().SingleInstance();
                    break;
            }
            
            container = buider.Build();
            return this;
        }

        public IContainer Register(Type type ,DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterType(type).InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterType(type).SingleInstance();
                    break;
            }
            container = buider.Build();
            return this;
        }

        public IContainer Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterType<TImpl>().As<TType>().InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterType<TImpl>().As<TType>().SingleInstance();
                    break;
            }
            container = buider.Build();
            return this;
        }

        public IContainer Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            switch (lifeStyle)
            {
                case DependencyLifeStyle.Transient:
                    buider.RegisterType(impl).InstancePerLifetimeScope();
                    break;
                case DependencyLifeStyle.Singleton:
                    buider.RegisterType(impl).SingleInstance();
                    break;
            }
            container = buider.Build();
            return this;
        }

        public void Release(object instance)
        {
            container.InjectUnsetProperties(instance);
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public T Resolve<T>(string key)
        {
            return container.ResolveKeyed<T>(key);
        }
        public object Resolve(Type service)
        {
            return container.Resolve(service);
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
            buider = null;
            container = null;
        }
    }
}
