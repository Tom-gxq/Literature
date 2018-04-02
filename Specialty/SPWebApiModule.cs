using Lib.Application.Services;
using Lib.Web.Api;
using Lib.WebApi.Controllers.Dynamic.Builders;
using LibMain.Modules;
using SP.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Specialty
{
    [DependsOn(typeof(WebApiModule), typeof(SPApplicationModule))]
    public class SPWebApiModule : LibMain.Modules.Module
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(SPApplicationModule).Assembly, "app")
                .Build();

        }
    }
}
