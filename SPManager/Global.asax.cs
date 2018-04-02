using Lib.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SPManager
{
    public class WebApiApplication : LibWebApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected override void Application_Start(object sender, EventArgs e)
        {
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            base.Application_Start(sender, e);
        }
    }
}
