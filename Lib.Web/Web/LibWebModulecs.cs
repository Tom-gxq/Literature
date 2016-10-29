using Lib.Web.Web.Configuration;
using LibMain;
using LibMain.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Lib.Web.Web
{
    [DependsOn(typeof(KernelModule))]
    public class LibWebModulecs : LibMain.Modules.Module
    {
        public override void PreInitialize()
        {
            if (HttpContext.Current != null)
            {
                XmlLocalizationSource.RootDirectoryOfApplication = HttpContext.Current.Server.MapPath("~");
            }

            IocManager.Register<IWebModuleConfiguration, WebModuleConfiguration>();

            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    AbpWebLocalizedMessages.SourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        Assembly.GetExecutingAssembly(), "Abp.Web.Localization.AbpWebXmlSource"
                        )));
        }

        /// <inheritdoc/>
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
