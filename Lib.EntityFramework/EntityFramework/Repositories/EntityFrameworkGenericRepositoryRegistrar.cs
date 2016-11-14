using Lib.EntityFramework.EntityFramework.Extensions;
using LibMain.Dependency;
using LibMain.Domain.Entities;
using LibMain.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework.Repositories
{
    internal static class EntityFrameworkGenericRepositoryRegistrar
    {
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager)
        {
            foreach (var entityType in dbContextType.GetEntityTypes())
            {
                foreach (var interfaceType in entityType.GetInterfaces())
                {
                    if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                    {
                        var primaryKeyType = interfaceType.GenericTypeArguments[0];
                        if (primaryKeyType == typeof(int))
                        {
                            var genericRepositoryType = typeof(IRepository<>).MakeGenericType(entityType);
                            if (!iocManager.IsRegistered(genericRepositoryType))
                            {
                                iocManager.Register(
                                    genericRepositoryType,
                                    typeof(ServiceStackRepositoryBase<,>).MakeGenericType(dbContextType, entityType),
                                    DependencyLifeStyle.Transient
                                    );
                            }
                        }

                        var genericRepositoryTypeWithPrimaryKey = typeof(IRepository<,>).MakeGenericType(entityType, primaryKeyType);
                        if (!iocManager.IsRegistered(genericRepositoryTypeWithPrimaryKey))
                        {
                            iocManager.Register(
                                genericRepositoryTypeWithPrimaryKey,
                                typeof(ServiceStackRepositoryBase<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType),
                                DependencyLifeStyle.Transient
                                );
                        }
                    }
                }
            }
        }
    }
}
