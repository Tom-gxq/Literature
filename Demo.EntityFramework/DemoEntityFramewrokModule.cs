using Demo.Core;
using Lib.EntityFramework.EntityFramework;
using LibMain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.EntityFramework
{
    [DependsOn(typeof(LibEntityFrameworkModule), typeof(DemoCoreModule))]
    public class DemoEntityFramewrokModule : LibMain.Modules.Module
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //执行一次.
            IocManager.Resolve<DemoDbContext>();
            //new DataInit().Build(IocManager.Resolve<AbpDemoDbContext>());
        }
    }
}
