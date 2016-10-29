using LibMain.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public interface IKernelInternal
    {
        // 摘要: 
        //     Internal logger used by the container (not just Castle.MicroKernel.IKernelInternal
        //     implementation itself but also other classes too) to log information about
        //     what's happening in the container.
        ILogger Logger { get; set; }

        // 摘要: 
        //     Adds a custom made Castle.Core.ComponentModel. Used by facilities.
        //
        // 参数: 
        //   model:
        IHandler AddCustomComponent(ComponentModel model);
        //
        // 摘要: 
        //     Constructs an implementation of Castle.MicroKernel.IComponentActivator for
        //     the given Castle.Core.ComponentModel
        //
        // 参数: 
        //   model:
        IComponentActivator CreateComponentActivator(ComponentModel model);
        IHandler CreateHandler(ComponentModel model);
        ILifestyleManager CreateLifestyleManager(ComponentModel model, IComponentActivator activator);
        IHandler LoadHandlerByName(string key, Type service, IDictionary arguments);
        IHandler LoadHandlerByType(string key, Type service, IDictionary arguments);
        IDisposable OptimizeDependencyResolution();
        void RaiseEventsOnHandlerCreated(IHandler handler);
        object Resolve(Type service, IDictionary arguments, IReleasePolicy policy);
        //
        // 摘要: 
        //     Returns a component instance by the key
        //
        // 参数: 
        //   key:
        //
        //   service:
        //
        //   arguments:
        //
        //   policy:
        object Resolve(string key, Type service, IDictionary arguments, IReleasePolicy policy);
        Array ResolveAll(Type service, IDictionary arguments, IReleasePolicy policy);
    }
}
