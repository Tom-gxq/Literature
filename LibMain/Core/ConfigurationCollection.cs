using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public class ConfigurationCollection : List<IConfiguration>
    {
        private Dictionary<string, IConfiguration> dic = new Dictionary<string, IConfiguration>();
        //
        // 摘要:
        //     Creates a new instance of ConfigurationCollection.
        public ConfigurationCollection()
        {

        }
        //
        // 摘要:
        //     Creates a new instance of ConfigurationCollection.
        public ConfigurationCollection(IEnumerable<IConfiguration> value)
        {

        }

        public IConfiguration this[string name]
        {
            get
            {
                return dic[name];
            }
        }
    }
}
