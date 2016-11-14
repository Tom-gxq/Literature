using Lib.EntityFramework.EntityFramework.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LibMain.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework.Extensions
{
    internal static class DbContextExtensions
    {
        public static IEnumerable<Type> GetEntityTypes(this Type dbContextType)
        {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) 
                select property.PropertyType.GenericTypeArguments[0];
        }
    }
}
