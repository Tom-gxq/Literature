using LibMain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Core
{
    public interface IConfigurationStore
    {
        // 摘要: 
        //     Adds the child container configuration.
        //
        // 参数: 
        //   name:
        //     The container's name.
        //
        //   config:
        //     The config.
        void AddChildContainerConfiguration(string name, IConfiguration config);
        //
        // 摘要: 
        //     Associates a configuration node with a component key
        //
        // 参数: 
        //   key:
        //     item key
        //
        //   config:
        //     Configuration node
        void AddComponentConfiguration(string key, IConfiguration config);
        //
        // 摘要: 
        //     Associates a configuration node with a facility key
        //
        // 参数: 
        //   key:
        //     item key
        //
        //   config:
        //     Configuration node
        void AddFacilityConfiguration(string key, IConfiguration config);
        void AddInstallerConfiguration(IConfiguration config);
        //
        // 摘要: 
        //     Returns the configuration node associated with the specified child container
        //     key. Should return null if no association exists.
        //
        // 参数: 
        //   key:
        //     item key
        IConfiguration GetChildContainerConfiguration(string key);
        //
        // 摘要: 
        //     Returns the configuration node associated with the specified component key.
        //     Should return null if no association exists.
        //
        // 参数: 
        //   key:
        //     item key
        IConfiguration GetComponentConfiguration(string key);
        //
        // 摘要: 
        //     Returns all configuration nodes for components
        IConfiguration[] GetComponents();
        //
        // 摘要: 
        //     Gets the child containers configuration nodes.
        IConfiguration[] GetConfigurationForChildContainers();
        //
        // 摘要: 
        //     Returns all configuration nodes for facilities
        IConfiguration[] GetFacilities();
        //
        // 摘要: 
        //     Returns the configuration node associated with the specified facility key.
        //     Should return null if no association exists.
        //
        // 参数: 
        //   key:
        //     item key
        IConfiguration GetFacilityConfiguration(string key);
        //
        // 摘要: 
        //     Returns all configuration nodes for installers
        IConfiguration[] GetInstallers();
        //
        //
        // 参数: 
        //   resourceUri:
        //
        //   resource:
        IResource GetResource(string resourceUri, IResource resource);
    }
}
