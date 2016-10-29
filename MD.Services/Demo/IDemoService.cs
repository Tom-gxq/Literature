using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Services.Demo
{
    public interface IDemoService
    {
        string ExcueteService();
        MD.Core.DomainModel.Demo GetDemoById(int id);
    }
}
