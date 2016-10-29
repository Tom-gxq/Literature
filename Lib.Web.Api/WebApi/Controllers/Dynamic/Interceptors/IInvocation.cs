using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Web.Api.WebApi.Controllers.Dynamic.Interceptors
{
    public interface IInvocation
    {
        //
        // 摘要:
        //     Gets the arguments that the Castle.DynamicProxy.IInvocation.Method has been invoked
        //     with.
        object[] Arguments { get; }
        //
        // 摘要:
        //     Gets the generic arguments of the method.
        Type[] GenericArguments { get; }
        //
        // 摘要:
        //     Gets the object on which the invocation is performed. This is different from
        //     proxy object because most of the time this will be the proxy target object.
        object InvocationTarget { get; }
        //
        // 摘要:
        //     Gets the System.Reflection.MethodInfo representing the method being invoked on
        //     the proxy.
        MethodInfo Method { get; }
        //
        // 摘要:
        //     For interface proxies, this will point to the System.Reflection.MethodInfo on
        //     the target class.
        MethodInfo MethodInvocationTarget { get; }
        //
        // 摘要:
        //     Gets the proxy object on which the intercepted method is invoked.
        object Proxy { get; }
        //
        // 摘要:
        //     Gets or sets the return value of the method.
        object ReturnValue { get; set; }
        //
        // 摘要:
        //     Gets the type of the target object for the intercepted method.
        Type TargetType { get; }

        //
        // 摘要:
        //     Gets the value of the argument at the specified index.
        //
        // 参数:
        //   index:
        //     The index.
        //
        // 返回结果:
        //     The value of the argument at the specified index.
        object GetArgumentValue(int index);
        //
        // 摘要:
        //     Returns the concrete instantiation of the Castle.DynamicProxy.IInvocation.Method
        //     on the proxy, with any generic parameters bound to real types.
        //
        // 返回结果:
        //     The concrete instantiation of the Castle.DynamicProxy.IInvocation.Method on the
        //     proxy, or the Castle.DynamicProxy.IInvocation.Method if not a generic method.
        //
        // 备注:
        //     Can be slower than calling Castle.DynamicProxy.IInvocation.Method.
        MethodInfo GetConcreteMethod();
        //
        // 摘要:
        //     Returns the concrete instantiation of Castle.DynamicProxy.IInvocation.MethodInvocationTarget,
        //     with any generic parameters bound to real types. For interface proxies, this
        //     will point to the System.Reflection.MethodInfo on the target class.
        //
        // 返回结果:
        //     The concrete instantiation of Castle.DynamicProxy.IInvocation.MethodInvocationTarget,
        //     or Castle.DynamicProxy.IInvocation.MethodInvocationTarget if not a generic method.
        //
        // 备注:
        //     In debug builds this can be slower than calling Castle.DynamicProxy.IInvocation.MethodInvocationTarget.
        MethodInfo GetConcreteMethodInvocationTarget();
        //
        // 摘要:
        //     Proceeds the call to the next interceptor in line, and ultimately to the target
        //     method.
        //
        // 备注:
        //     Since interface proxies without a target don't have the target implementation
        //     to proceed to, it is important, that the last interceptor does not call this
        //     method, otherwise a System.NotImplementedException will be thrown.
        void Proceed();
        //
        // 摘要:
        //     Overrides the value of an argument at the given index with the new value provided.
        //
        // 参数:
        //   index:
        //     The index of the argument to override.
        //
        //   value:
        //     The new value for the argument.
        //
        // 备注:
        //     This method accepts an System.Object, however the value provided must be compatible
        //     with the type of the argument defined on the method, otherwise an exception will
        //     be thrown.
        void SetArgumentValue(int index, object value);
    }
}
