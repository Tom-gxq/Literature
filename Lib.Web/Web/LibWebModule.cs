using Lib.Web.Configuratio;
using Lib.Web.Web.Configuratio;
using Lib.Web.Web.Localization;
using LibMain.Localization.Sources;
using LibMain.Localization.Sources.Xml;
using LibMain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lib.Web
{
    public class LibWebModule: LibMain.Modules.Module
    {
        /// <inheritdoc/>
        public override void PreInitialize()
        {
            if (HttpContext.Current != null)
            {
                XmlLocalizationSource.RootDirectoryOfApplication = HttpContext.Current.Server.MapPath("~");
            }

            IocManager.Register<ILibWebModuleConfiguration, LibWebModuleConfiguration>();
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    LibWebLocalizedMessages.SourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Lib.Web.Localization.LibWebXmlSource"
                        )));
        }
    }
}
