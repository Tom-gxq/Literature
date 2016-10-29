using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MD.Core.Infrastructure.DependencyManagement;
using MD.Data;
using MD.Web.Frameworker;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MD.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        IScheduler sched = null;
        protected void Application_Start()
        { 
            AreaRegistration.RegisterAllAreas();
            var builder = new ContainerBuilder();
            
            IDependencyRegistrar dependency = new DependencyRegistrar();
            dependency.Register(builder);
            var container = builder.Build();
             var obj = container.Resolve<Services.Demo.IDemoService>();
            container.InjectUnsetProperties(obj);

            //set dependency resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));//注册MVC容器
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);//注册api容器

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<MDObjectContext>());

            //定时任务
            ISchedulerFactory sf = new StdSchedulerFactory();
            sched = sf.GetScheduler();
            IJobDetail job = JobBuilder.Create<TimingTask>().WithIdentity("sport_quan_job", "sport_quan_group").Build();
            string cronExpr = ConfigurationManager.AppSettings.Get("CronExpr");
            ITrigger trigger = TriggerBuilder.Create().WithIdentity("sport_quan_job", "sport_quan_group").WithCronSchedule(cronExpr).Build();
            sched.ScheduleJob(job, trigger);
            sched.Start();
        }

        private void Application_End(object sender, EventArgs e)
        {
            // 在应用程序关闭时运行的代码 
            if (sched != null) { sched.Shutdown(true); }
        }
    }
}
