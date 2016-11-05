using Castle.MicroKernel.Registration;
using Castle.Windsor;
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
        private IWindsorContainer container;        
        public LibMainContainer()
        {
            container = new WindsorContainer();
        }

        public IContainer Install(params IInstaller[] installers)
        {
            container.Install(installers);
            return this;
        }

        public bool IsRegistered(Type type)
        {
            if (this.container != null)
            {
                return this.container.Kernel.HasComponent(type);
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
                return this.container.Kernel.HasComponent(typeof(TType));
            }
            else
            {
                return false;
            }
        }
        

        public IContainer Register(Type type ,DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            //var buider = new ContainerBuilder();
            //switch (lifeStyle)
            //{
            //    case DependencyLifeStyle.Transient:
            //        buider.RegisterType(type).InstancePerLifetimeScope();
            //        break;
            //    case DependencyLifeStyle.Singleton:
            //        buider.RegisterType(type).SingleInstance();
            //        break;
            //}
            //buider.Update(this.container);
            return this;
        }

        public IContainer Register<TType, TImpl>(DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TType : class
            where TImpl : class, TType
        {
            //var buider = new ContainerBuilder();
            //switch (lifeStyle)
            //{
            //    case DependencyLifeStyle.Transient:
            //        buider.RegisterType<TImpl>().As<TType>().InstancePerLifetimeScope();
            //        break;
            //    case DependencyLifeStyle.Singleton:
            //        buider.RegisterType<TImpl>().As<TType>().SingleInstance();
            //        break;
            //}
            //buider.Update(this.container);
            //var obj = this.container.Resolve<TType>();
            //this.container.InjectUnsetProperties(obj);
            return this;
        }
        
        public IContainer RegisterInstance<TType, TImpl>(TImpl instance, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
            where TImpl : class, TType
        {
            //var buider = new ContainerBuilder();
            //switch (lifeStyle)
            //{
            //    case DependencyLifeStyle.Transient:
            //        buider.RegisterInstance<TImpl>(instance).As<TType>().InstancePerLifetimeScope();
            //        break;
            //    case DependencyLifeStyle.Singleton:
            //        buider.RegisterInstance<TImpl>(instance).As<TType>().SingleInstance();
            //        break;
            //}
            //buider.Update(this.container);
            //var obj = this.container.Resolve<TType>();
            //this.container.InjectUnsetProperties(obj);
            return this;
        }

        public IContainer Register(Type type, Type impl, DependencyLifeStyle lifeStyle = DependencyLifeStyle.Singleton)
        {
            //var buider = new ContainerBuilder();
            //switch (lifeStyle)
            //{
            //    case DependencyLifeStyle.Transient:
            //        buider.RegisterType(impl).InstancePerLifetimeScope();
            //        break;
            //    case DependencyLifeStyle.Singleton:
            //        buider.RegisterType(impl).SingleInstance();
            //        break;
            //}
            //buider.Update(this.container);
            return this;
        }

        public IContainer Register(params IRegistration[] registrations)
        {
            container.Register(registrations);
            return this;
        }

        public void Release(object instance)
        {
            container.Release(instance);
        }

        public T Resolve<T>()
        {
            try
            {
                return container.Resolve<T>();
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public T Resolve<T>(object argumentsAsAnonymousType)
        {
            //return container.ResolveKeyed<T>(key);
            return container.Resolve<T>(argumentsAsAnonymousType);
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// </summary> 
        /// <param name="type">Type of the object to get</param>
        /// <returns>The instance object</returns>
        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        /// <summary>
        /// Gets an object from IOC container.
        /// Returning object must be Released (see <see cref="IIocResolver.Release"/>) after usage.
        /// </summary> 
        /// <param name="type">Type of the object to get</param>
        /// <param name="argumentsAsAnonymousType">Constructor arguments</param>
        /// <returns>The instance object</returns>
        public object Resolve(Type type, object argumentsAsAnonymousType)
        {
            return container.Resolve(type, argumentsAsAnonymousType);
        }
        public void Dispose()
        {
            container.Dispose();
        }
    }
}
