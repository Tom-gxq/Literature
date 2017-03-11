using LibMain.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Runtime.Session
{
    /// <summary>
    /// Defines some session information that can be useful for applications.
    /// </summary>
    public interface ILibSession
    {
        /// <summary>
        /// Gets current UserId or null.
        /// </summary>
        string SessionID { get; }
        void Remove(string key);
        void Clear();
        object this[string key] { get; set; }
    }
}
