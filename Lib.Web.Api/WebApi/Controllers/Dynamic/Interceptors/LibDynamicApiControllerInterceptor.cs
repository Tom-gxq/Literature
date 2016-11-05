using Lib.WebApi.Controllers.Dynamic.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Web.Api.WebApi.Controllers.Dynamic.Interceptors
{
    /// <typeparam name="T">Type of the proxied object</typeparam>
    public class LibDynamicApiControllerInterceptor<T> : IInterceptor
    {
        /// <summary>
        /// Real object instance to call it's methods when dynamic controller's methods are called.
        /// </summary>
        private readonly T _proxiedObject;

        /// <summary>
        /// Creates a new AbpDynamicApiControllerInterceptor object.
        /// </summary>
        /// <param name="proxiedObject">Real object instance to call it's methods when dynamic controller's methods are called</param>
        public LibDynamicApiControllerInterceptor(T proxiedObject)
        {
            _proxiedObject = proxiedObject;
        }

        /// <summary>
        /// Intercepts method calls of dynamic api controller
        /// </summary>
        /// <param name="invocation">Method invocation information</param>
        public void Intercept(Castle.DynamicProxy.IInvocation invocation)
        {
            //If method call is for generic type (T)...
            if (DynamicApiControllerActionHelper.IsMethodOfType(invocation.Method, typeof(T)))
            {
                //Call real object's method
                try
                {
                    invocation.ReturnValue = invocation.Method.Invoke(_proxiedObject, invocation.Arguments);
                }
                catch (TargetInvocationException targetInvocation)
                {
                    if (targetInvocation.InnerException != null)
                    {
                        //targetInvocation.InnerException.ReThrow();
                    }

                    throw;
                }
            }
            else
            {
                //Call api controller's methods as usual.
                invocation.Proceed();
            }
        }
    }
}
