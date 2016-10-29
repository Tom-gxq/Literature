using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public interface IConfiguration
    {
        // 摘要: 
        //     Gets an System.Collections.IDictionary of the configuration attributes.
        ConfigurationAttributeCollection Attributes { get; }
        //
        // 摘要: 
        //     Gets an Castle.Core.Configuration.ConfigurationCollection of Castle.Core.Configuration.IConfiguration
        //     elements containing all node children.
        ConfigurationCollection Children { get; }
        //
        // 摘要: 
        //     Gets the name of the node.
        string Name { get; }
        //
        // 摘要: 
        //     Gets the value of the node.
        string Value { get; }

        // 摘要: 
        //     Gets the value of the node and converts it into specified System.Type.
        //
        // 参数: 
        //   type:
        //     The System.Type
        //
        //   defaultValue:
        //     The Default value returned if the conversion fails.
        //
        // 返回结果: 
        //     The Value converted into the specified type.
        object GetValue(Type type, object defaultValue);
    }
}
