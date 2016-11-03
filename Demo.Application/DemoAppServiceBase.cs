using Demo.Core;
using Lib.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application
{
    public class DemoAppServiceBase: ApplicationService
    {
        protected DemoAppServiceBase()
        {
            LocalizationSourceName = DemoConsts.LocalizationSourceName;
        }
    }
}
