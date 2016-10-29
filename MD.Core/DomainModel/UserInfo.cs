using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Core.DomainModel
{
    public partial class UserInfo : BaseEntity
    {
        public string Name { get; set; }
        public bool Sex {get;set;}
        public DateTime Birthday { get; set; }

        public string Job { get; set; }

        public string Company { get; set; }

        public int Weight { get; set; }
        public int Height { get; set; }
        public string QQ { get; set; }

        public string Weixin { get; set; }

        public string Introduction { get; set; }

        public Account Account { get; set; }

    }
}
