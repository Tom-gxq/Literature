using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.OData.Builder;

namespace Lib.Web.Api.OData.WebApi.OData.Configuration
{
    public interface ILibWebApiODataModuleConfiguration
    {
        /// <summary>
        /// Gets ODataConventionModelBuilder.
        /// </summary>
        ODataConventionModelBuilder ODataModelBuilder { get; }
    }
}
