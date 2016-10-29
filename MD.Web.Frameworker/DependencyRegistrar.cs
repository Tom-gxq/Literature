using Autofac;
using Autofac.Integration.Mvc;
using MD.Core.Data;
using MD.Core.Infrastructure.DependencyManagement;
using MD.Data;
using MD.Services.Demo;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac.Integration.WebApi;
using MD.Services.Register;

namespace MD.Web.Frameworker
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder)
        {
            //controllers layer
            Assembly controllerAssembly = Assembly.Load("MD.Web");            
            builder.RegisterControllers(controllerAssembly);
            builder.RegisterApiControllers(controllerAssembly);

            //data layer
            string dataConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            builder.Register<IDbContext>(c => new MDObjectContext(dataConnectionString)).InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //service layer
            builder.RegisterType<DemoService>().As<IDemoService>().InstancePerLifetimeScope();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();

            
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
