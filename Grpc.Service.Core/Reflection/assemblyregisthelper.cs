using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Castle.MicroKernel.Registration;
using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Grpc.Service.Core.Domain.Messaging;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Repositories.Extensions;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Reporting;
using Grpc.Service.Core.Domain.Sender;

namespace Grpc.Service.Core.Reflection
{
    public static class AssemblyRegistHelper
    {
        private static readonly ITypeFinder _typeFinder = new Grpc.Service.Core.Reflection.TypeFinder(CurrentDomainAssemblyFinder.Instance);
        public static void Register(IConfigurationRoot config)
        {
            IocManager.Instance.Register<IConfigurationRoot>(config, DependencyLifeStyle.Singleton);
            RegisterGenericRepositories();
        }

        private static void RegisterGenericRepositories()
        {            
            var dllTypes =
                _typeFinder.Find(type =>
                    type.GetTypeInfo().IsPublic &&
                    !type.GetTypeInfo().IsAbstract &&
                    type.GetTypeInfo().IsClass 
                    );

            if (dllTypes == null)
            {
                return;
            }

            foreach (var type in dllTypes)
            {
                if (typeof(IDataContext).IsAssignableFrom(type))
                {
                    //EntityFrameworkGenericRepositoryRegistrar.RegisterForDbContext(dbContextType, IocManager.Instance);
                    IocManager.Instance.Register(typeof(IDataContext), type, DependencyLifeStyle.Singleton);
                }
                else if(type.GetTypeInfo().GetInterface(typeof(IRepository<>).FullName) != null)
                {
                    IocManager.Instance.Register(type, DependencyLifeStyle.Singleton);
                }
                else if (type.GetTypeInfo().GetInterface(typeof(ICommandHandlerFactory).FullName) != null)
                {
                    IocManager.Instance.Register(typeof(ICommandHandlerFactory),type, DependencyLifeStyle.Singleton);
                }
                else if (type.GetTypeInfo().GetInterface(typeof(IEventHandlerFactory).FullName) != null)
                {
                    IocManager.Instance.Register(typeof(IEventHandlerFactory), type, DependencyLifeStyle.Singleton);
                }
                else if (type.GetTypeInfo().GetInterface(typeof(ICommandBus).FullName) != null)
                {
                    IocManager.Instance.Register(typeof(ICommandBus), type, DependencyLifeStyle.Singleton);
                }
                else if (type.GetTypeInfo().GetInterface(typeof(IEventBus).FullName) != null)
                {
                    IocManager.Instance.Register(typeof(IEventBus), type, DependencyLifeStyle.Singleton);
                }
                else if (type.GetTypeInfo().GetInterface(typeof(ICommandHandler).FullName) != null 
                    || type.GetTypeInfo().GetInterface(typeof(IEventHandler).FullName) != null)
                {
                    IocManager.Instance.Register(type, DependencyLifeStyle.Transient);
                }
                else if (type.GetTypeInfo().GetInterface(typeof(IAggregateRoot<>).FullName) != null)
                {
                    RegisterForIDatatRepository(type, IocManager.Instance);
                }
                else if (type.GetTypeInfo().GetInterface(typeof(IReportDatabase).FullName) != null)
                {
                    IocManager.Instance.Register(type, DependencyLifeStyle.Singleton);
                }
                else if (type.GetTypeInfo().GetInterface(typeof(ISender).FullName) != null)
                {
                    IocManager.Instance.Register(type, DependencyLifeStyle.Singleton);
                }
            }
            RegisterForIEventStorage(IocManager.Instance);

        }
        public static void RegisterForIDatatRepository(Type dbContextType, IIocManager iocManager)
        {
            var autoRepository = typeof(IDataRepository<>);


            foreach (var interfaceType in dbContextType.GetInterfaces())
            {
                var primaryKeyType = interfaceType.GenericTypeArguments.Count() > 0? interfaceType.GenericTypeArguments[0]:typeof(string);
                if (primaryKeyType == typeof(Guid) && interfaceType == typeof(IAggregateRoot<Guid>))
                {                    
                    var genericRepositoryType = autoRepository.MakeGenericType(dbContextType);
                    if (!iocManager.IsRegistered(genericRepositoryType))
                    {
                        var implTypes =
                            _typeFinder.Find(type =>
                                type.GetTypeInfo().IsPublic &&
                                !type.GetTypeInfo().IsAbstract &&
                                type.GetTypeInfo().IsClass &&
                                type.GetTypeInfo().GetInterface(autoRepository.FullName) != null
                                );
                        foreach (var implType in implTypes)
                        {
                            var impl = implType.MakeGenericType(dbContextType);

                            iocManager.Register(
                                genericRepositoryType,
                                impl,
                                DependencyLifeStyle.Transient
                                );
                        }
                        
                    }                    

                }
            }
        }

        public static void RegisterForIEventStorage(IIocManager iocManager)
        {
            var eventStorage = typeof(IEventStorage);
            var implTypes =_typeFinder.Find(type =>
                                type.GetTypeInfo().IsPublic &&
                                !type.GetTypeInfo().IsAbstract &&
                                type.GetTypeInfo().IsClass &&
                                type.GetTypeInfo().GetInterface(eventStorage.FullName) != null
                                );
            foreach (var implType in implTypes)
            {
                iocManager.Register(
                    eventStorage,
                    implType,
                    DependencyLifeStyle.Transient
                    );
            }
        }

    }

    
}
