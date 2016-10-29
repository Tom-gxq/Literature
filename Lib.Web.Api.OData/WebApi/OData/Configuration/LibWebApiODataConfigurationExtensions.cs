using LibMain.Configuration.Startup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Web.Api.OData.WebApi.OData.Configuration
{
    /// <summary>
    /// Defines extension methods to <see cref="IModuleConfigurations"/> to allow to configure Abp.Web.Api.OData module.
    /// </summary>
    public static class LibWebApiODataConfigurationExtensions
    {
        /// <summary>
        /// Used to configure Abp.Web.Api.OData module.
        /// </summary>
        public static ILibWebApiODataModuleConfiguration AbpWebApiOData(this IModuleConfigurations configurations)
        {
            return configurations.Configuration.GetOrCreate("Modules.Abp.Web.Api.OData", () => configurations.Configuration.IocManager.Resolve<ILibWebApiODataModuleConfiguration>());
        }
    }
}
