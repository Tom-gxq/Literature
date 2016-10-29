using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public class FromAssembly
    {
        public static IInstaller[] Instance(Assembly assembly)
        {
            List<IInstaller>installers = new List<IInstaller>();
            Type[] types = assembly.GetTypes();
            foreach(var type in types)
            {
                if (typeof(IInstaller).IsAssignableFrom(type))
                {
                    installers.Add((IInstaller)Activator.CreateInstance(type));
                }
            }
            return installers.ToArray();
        }
    }
}
