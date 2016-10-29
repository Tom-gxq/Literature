using LibMain.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Lib.WebApi.Controllers
{
    /// <summary>
    /// This class is used to use IOC system to create api controllers.
    /// It's used by ASP.NET system.
    /// </summary>
    public class LibControllerActivator : IHttpControllerActivator
    {
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            var controllerWrapper = IocManager.Instance.ResolveAsDisposable<IHttpController>(controllerType);
            request.RegisterForDispose(controllerWrapper);
            return controllerWrapper.Object;
        }
    }
}
