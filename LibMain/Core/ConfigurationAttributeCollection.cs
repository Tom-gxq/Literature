using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public class ConfigurationAttributeCollection : NameValueCollection
    {
        public ConfigurationAttributeCollection()
        {

        }
        protected ConfigurationAttributeCollection(SerializationInfo info, StreamingContext context)
        {

        }
    }
}
