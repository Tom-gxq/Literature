using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using Grpc.Service.Core.Reflection;
using Grpc.Service.Core.Domain.Entity;

namespace Grpc.Service.Core.Domain.Repositories.Extensions
{
    internal static class DbContextExtensions
    {
        public static IEnumerable<TypeInfo> GetEntityTypes(this TypeInfo dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>))                    
                select property.PropertyType.GenericTypeArguments[0].GetTypeInfo();
        }
    }
}
