using Lib.Web.Api.Configuration.Startup;
using Lib.Web.Api.OData.WebApi.OData.Configuration;
using LibMain.Dependency;
using LibMain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.OData;
using System.Web.OData.Extensions;

namespace Lib.Web.Api.OData.WebApi.OData
{
    public class LibWebApiODataModule : LibMain.Modules.Module
    {
        public override void PreInitialize()
        {
            IocManager.Register<ILibWebApiODataModuleConfiguration, LibWebApiODataModuleConfiguration>();
        }

        public override void Initialize()
        {
            IocManager.Register<MetadataController>(DependencyLifeStyle.Transient);
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Modules.LibWebApi().HttpConfiguration.MapODataServiceRoute(
                    routeName: "ODataRoute",
                    routePrefix: "odata",
                    model: Configuration.Modules.AbpWebApiOData().ODataModelBuilder.GetEdmModel()
                );
        }
    }
}
