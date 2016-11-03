using Demo.Application;
using Lib.Application.Services;
using Lib.Web.Api;
using Lib.WebApi.Controllers.Dynamic.Builders;
using LibMain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.WebApi
{
    [DependsOn(typeof(WebApiModule), typeof(DemoApplicationModule))]
    public class DemoWebApiModule : LibMain.Modules.Module
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(DemoApplicationModule).Assembly, "app")
                .Build();

        }
    }
}
