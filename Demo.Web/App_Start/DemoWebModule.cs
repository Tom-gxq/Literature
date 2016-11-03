using LibMain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Application;
using Demo.WebApi;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Routing;
using System.Web.Optimization;

namespace Demo.Web
{
    [DependsOn(typeof(DemoApplicationModule), typeof(DemoWebApiModule))]
    public class DemoWebModule: LibMain.Modules.Module
    {
        public override void PreInitialize()
        {

        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
