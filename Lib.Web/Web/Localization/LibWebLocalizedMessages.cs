using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Web.Web.Localization
{
    /// <summary>
    /// This class is used to simplify getting localized messages in this assembly.
    /// </summary>
    internal static class LibWebLocalizedMessages
    {
        public const string SourceName = "AbpWeb";

        public static string InternalServerError { get { return L("InternalServerError"); } }

        public static string ValidationError { get { return L("ValidationError"); } }

        //private static readonly ILocalizationSource Source;

        static LibWebLocalizedMessages()
        {
            //Source = LocalizationHelper.GetSource(SourceName);
        }

        private static string L(string name)
        {
            try
            {
                //return Source.GetString(name);
                return "";
            }
            catch (Exception)
            {
                return name;
            }
        }
    }
}
