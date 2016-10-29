using Lib.Web.Api.WebApi.Configuration;
using LibMain.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Web.Api.Configuration.Startup
{
    public static class WebApiConfigurationExtensions
    {
        public static IWebApiModuleConfiguration LibWebApi(this IModuleConfigurations configurations)
        {
            return configurations.Configuration.GetOrCreate("Modules.Web.Api", () => configurations.Configuration.IocManager.Resolve<IWebApiModuleConfiguration>());
        }
    }
}
