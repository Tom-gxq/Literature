using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD.Core.DomainModel;
using MD.Core.Data;

namespace MD.Services.Demo
{
    public partial class DemoService :IDemoService
    {
        private readonly IRepository<MD.Core.DomainModel.Demo> _demoRepository;

        public DemoService(IRepository<MD.Core.DomainModel.Demo> demoRepository)
        {
            this._demoRepository = demoRepository;
        }
        public string ExcueteService()
        {
            MD.Core.DomainModel.Demo demo = new Core.DomainModel.Demo();
            demo.Id = 2;
            demo.Name ="demoName";
            this._demoRepository.Insert(demo);
            return "ExcueteService";
        }

        public MD.Core.DomainModel.Demo GetDemoById(int id)
        {
            return this._demoRepository.GetById(id);
        }
    }
}
