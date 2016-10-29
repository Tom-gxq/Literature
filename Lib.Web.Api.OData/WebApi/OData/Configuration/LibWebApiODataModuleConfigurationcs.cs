using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.OData.Builder;

namespace Lib.Web.Api.OData.WebApi.OData.Configuration
{
    internal class LibWebApiODataModuleConfiguration : ILibWebApiODataModuleConfiguration
    {
        public ODataConventionModelBuilder ODataModelBuilder { get; private set; }

        public LibWebApiODataModuleConfiguration()
        {
            ODataModelBuilder = new ODataConventionModelBuilder();
        }
    }
}
