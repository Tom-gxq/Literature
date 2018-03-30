using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Grpc.Service.Core.Reflection
{
    /// <summary>
    /// Default implementation of <see cref="IAssemblyFinder"/>.
    /// If gets assemblies from current domain.
    /// </summary>
    public class CurrentDomainAssemblyFinder : IAssemblyFinder
    {
        /// <summary>
        /// Gets Singleton instance of <see cref="CurrentDomainAssemblyFinder"/>.
        /// </summary>
        public static CurrentDomainAssemblyFinder Instance { get { return SingletonInstance; } }
        private static readonly CurrentDomainAssemblyFinder SingletonInstance = new CurrentDomainAssemblyFinder();

        public List<Assembly> GetAllAssemblies()
        {
            var assembliesInBinFolder = new List<Assembly>();
            
            var dllFiles = Directory.GetFiles(Directory.GetCurrentDirectory() , "*.dll", SearchOption.AllDirectories).ToList();
            var currentAssembly = Assembly.GetEntryAssembly();
            string curentDll = string.Empty;
            if(currentAssembly != null)
            {
                int lastIndex = currentAssembly.Location.LastIndexOf(Path.DirectorySeparatorChar) +1;
                curentDll = currentAssembly.Location?.Substring(lastIndex, currentAssembly.Location.Length- lastIndex);
            }
            
            foreach (string dllFile in dllFiles)
            {
                int lastIndex = dllFile.LastIndexOf(Path.DirectorySeparatorChar)+1;
                var findDll = dllFile.Substring(lastIndex, dllFile.Length- lastIndex);
                var obj = assembliesInBinFolder.Find(x => x.ManifestModule.Name == findDll);
                             
                if (curentDll != findDll && (findDll.StartsWith("Grpc.") || findDll.StartsWith("SP.")) && (obj == null) )
                {                    
                    var locatedAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllFile);
                    if (locatedAssembly != null)
                    {
                        assembliesInBinFolder.Add(locatedAssembly);
                    }
                }
            }            

            return assembliesInBinFolder;
        }
    }
}