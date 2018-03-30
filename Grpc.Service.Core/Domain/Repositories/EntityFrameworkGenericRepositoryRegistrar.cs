using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.Dependency;
using System.Reflection;
using Grpc.Service.Core.Domain.Repositories.Extensions;
using Grpc.Service.Core.Domain.Entity;

namespace Grpc.Service.Core.Domain.Repositories
{
    internal static class EntityFrameworkGenericRepositoryRegistrar
    {
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            var autoRepository = typeof(IRepository<>);
            

            foreach (var entityType in dbContextType.GetTypeInfo().GetEntityTypes())
            {
                foreach (var interfaceType in entityType.GetInterfaces())
                {
                    if (interfaceType == typeof(IEntity<>))
                    {
                        var genericRepositoryType = autoRepository.MakeGenericType(entityType.GetType());
                        if (!iocManager.IsRegistered(genericRepositoryType))
                        {
                            var implType = typeof(EfRepository<>).MakeGenericType(dbContextType, entityType.GetType());

                            iocManager.Register(
                                genericRepositoryType,
                                implType,
                                DependencyLifeStyle.Transient
                                );
                        }

                    }
                }
            }
        }
    }
}
