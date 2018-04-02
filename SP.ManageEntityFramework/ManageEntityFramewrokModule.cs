using Lib.EntityFramework.EntityFramework;
using LibMain.Dependency;
using LibMain.Modules;
using SP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework
{
    [DependsOn(typeof(LibEntityFrameworkModule), typeof(SPCoreModule))]
    public class ManageEntityFramewrokModule : LibMain.Modules.Module
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "ManageSP";
        }

        public override void Initialize()
        {
            var config = new ConventionalRegistrationConfig
            {
                InstallInstallers = false
            };
            config["nameOrConnectionString"] = "ManageSP";
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly(), config);
            //执行一次.
            IocManager.Resolve<ManageDbContext>();
        }
    }
}
