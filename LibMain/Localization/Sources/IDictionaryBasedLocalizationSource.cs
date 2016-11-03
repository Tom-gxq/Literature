using LibMain.Localization.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Localization.Sources
{
    /// <summary>
    /// Interface for a dictionary based localization source.
    /// </summary>
    public interface IDictionaryBasedLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// Extends the source with given dictionary.
        /// </summary>
        /// <param name="dictionary">Dictionary to extend the source</param>
        void Extend(ILocalizationDictionary dictionary);
    }
}
