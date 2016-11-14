using LibMain;
using LibMain.Runtime.Session;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework
{
    /// <summary>
    /// Base class for all DbContext classes in the application.
    /// </summary>
    public abstract class LibDbContext : ILibDbContext, IShouldInitialize
    {
        private string nameOrConnectionString;
        /// <summary>
        /// Constructor.
        /// Uses <see cref="IAbpStartupConfiguration.DefaultNameOrConnectionString"/> as connection string.
        /// </summary>
        protected LibDbContext()
        {
            
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected LibDbContext(string nameOrConnectionString)
        {
            this.nameOrConnectionString = nameOrConnectionString;
        }

        

        public virtual void Initialize()
        {
        }

        public IDbConnection OpenDbConnection()
        {
            string DbConnection = ConfigurationManager.ConnectionStrings[this.nameOrConnectionString].ConnectionString;
            if (DbConnection == null)
            {
                throw new Exception("Connection string \"DbConnection\" can not be null.");
            }

            return DbConnection.OpenDbConnection();
        }
    }
}
