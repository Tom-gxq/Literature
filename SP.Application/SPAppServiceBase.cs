using Lib.Application.Services;
using SP.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application
{
    public class SPAppServiceBase : ApplicationService
    {
        protected SPAppServiceBase()
        {
            LocalizationSourceName = SPConsts.LocalizationSourceName;
        }
    }
}
