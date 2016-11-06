using LibMain.MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Runtime.Session
{
    public class NullLibSession:ILibSession
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullLibSession Instance { get { return SingletonInstance; } }
        private static readonly NullLibSession SingletonInstance = new NullLibSession();

        /// <inheritdoc/>
        public long? UserId { get { return null; } }

        /// <inheritdoc/>
        public int? TenantId { get { return null; } }

        public MultiTenancySides MultiTenancySide { get { return MultiTenancySides.Tenant; } }

        private NullLibSession()
        {

        }
    }
}
