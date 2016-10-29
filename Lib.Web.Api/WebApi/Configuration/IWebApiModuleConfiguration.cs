using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Lib.Web.Api.WebApi.Configuration
{
    /// <summary>
    /// Used to configure ABP WebApi module.
    /// </summary>
    public interface IWebApiModuleConfiguration
    {
        /// <summary>
        /// Gets/sets <see cref="HttpConfiguration"/>.
        /// </summary>
        HttpConfiguration HttpConfiguration { get; set; }
    }
}
