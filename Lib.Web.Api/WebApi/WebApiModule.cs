using Lib.WebApi.Controllers;
using Lib.WebApi.Controllers.Dynamic;
using Lib.WebApi.Controllers.Dynamic.Formatters;
using Lib.WebApi.Controllers.Dynamic.Selectors;
using Lib.WebApi.Controllers.Filters;
using LibMain.Dependency;
using LibMain.Modules;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Lib.Web.Api
{

    [DependsOn(typeof(WebApiModule))]
    public class WebApiModule: LibMain.Modules.Module
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            IocManager.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            InitializeAspNetServices();
            InitializeFilters();
            InitializeFormatters();
            InitializeRoutes();
        }

        private static void InitializeAspNetServices()
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerSelector), new LibHttpControllerSelector(GlobalConfiguration.Configuration));
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpActionSelector), new LibApiControllerActionSelector());
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new LibControllerActivator());
        }

        private void InitializeFilters()
        {
            GlobalConfiguration.Configuration.Filters.Add(IocManager.Resolve<LibExceptionFilterAttribute>());
        }

        private static void InitializeFormatters()
        {
            GlobalConfiguration.Configuration.Formatters.Clear();
            var formatter = new JsonMediaTypeFormatter();
            formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            GlobalConfiguration.Configuration.Formatters.Add(formatter);
            GlobalConfiguration.Configuration.Formatters.Add(new PlainTextFormatter());
        }

        private static void InitializeRoutes()
        {
            DynamicApiRouteConfig.Register();
        }
    }
}
