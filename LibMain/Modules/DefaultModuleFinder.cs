using LibMain.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Modules
{
    internal class DefaultModuleFinder : IModuleFinder
    {
        private readonly ITypeFinder _typeFinder;

        public DefaultModuleFinder(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public ICollection<Type> FindAll()
        {
            return _typeFinder.Find(CustomBaseModule.IsAbpModule).ToList();
        }
    }
}
