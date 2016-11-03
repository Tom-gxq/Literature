using Lib.Web.Configuratio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Web.Web.Configuratio
{
    internal class LibWebModuleConfiguration : ILibWebModuleConfiguration
    {
        public bool SendAllExceptionsToClients { get; set; }
    }
}
