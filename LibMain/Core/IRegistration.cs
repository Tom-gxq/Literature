using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public interface IRegistration
    {
        // 摘要: 
        //     Performs the registration in the Castle.MicroKernel.IKernel.
        //
        // 参数: 
        //   kernel:
        //     The kernel.
        void Register(IKernelInternal kernel);
    }
}
