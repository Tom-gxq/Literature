using Grpc.Service.Core;
using Microsoft.Extensions.Configuration;
using System;

namespace SP.Service.EntityFramework
{
    public class DataContext : IDataContext
    {
        private IConfigurationRoot config;
        public DataContext(IConfigurationRoot config)
        {
            this.config = config;
        }

        public string GetConnectionString()
        {
            return this.config.GetSection("ConnectionString_SP").Value;
        }
    }
}
