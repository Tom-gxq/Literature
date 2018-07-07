using LibMain.Modules;
using SP.Application;
using SP.ManageEntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SPManager.App_Start
{
    [DependsOn(typeof(ManageEntityFramewrokModule), typeof(SPApplicationModule))]
    public class SPWebModule : LibMain.Modules.Module
    {
        public override void PreInitialize()
        {
            int i = 0;
            i++;
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