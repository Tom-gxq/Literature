using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class RegionDataModel
    {
        public int dataId { get; set; }
        public string dataName { get; set; }
        public int parentDataId { get; set; }
        public List<RegionDataModel> childList { get; set; }
    }
}
