using Lib.Web.Models;
using Lib.Web.Web.Models;
using LibMain.Dependency;
using LibMain.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Lib.WebApi.Controllers.Filters
{
    /// <summary>
    /// Used to handle exceptions on web api controllers.
    /// </summary>
    public class LibExceptionFilterAttribute : ExceptionFilterAttribute, ITransientDependency
    {
        public ILogger Logger { get; set; }

        //public IEventBus EventBus { get; set; }

        public LibExceptionFilterAttribute()
        {
            Logger = NullLogger.Instance;
            //EventBus = NullEventBus.Instance;
        }

        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="context">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext context)
        {
            //LogHelper.LogException(Logger, context.Exception);

            context.Response = context.Request.CreateResponse(
                HttpStatusCode.OK,
                new AjaxResponse(
                    ErrorInfoBuilder.Instance.BuildForException(context.Exception),
                    //context.Exception is Abp.Authorization.AbpAuthorizationException)
                    false)
                );

            //EventBus.Trigger(this, new AbpHandledExceptionData(context.Exception));
        }
    }
}
