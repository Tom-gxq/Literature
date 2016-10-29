using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public interface IInstaller
    {       
        void Install(IContainer container);
    }
}
