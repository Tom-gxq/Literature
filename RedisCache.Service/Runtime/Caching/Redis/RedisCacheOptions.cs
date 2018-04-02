using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisCache.Service.Runtime.Caching.Redis
{
    public class RedisCacheOptions
    {
        public IConfigurationRoot Configuration { get; private set; }

        private const string ConnectionStringKey = "Redis.Cache";

        private const string DatabaseIdSettingKey = "Redis.Cache.DatabaseId";

        public string ConnectionString { get; set; }

        public int DatabaseId { get; set; }

        public RedisCacheOptions(IConfigurationRoot startupConfiguration)
        {
            Configuration = startupConfiguration;

            ConnectionString = GetDefaultConnectionString();
            DatabaseId = GetDefaultDatabaseId();
        }

        private int GetDefaultDatabaseId()
        {
            var appSetting = this.Configuration.GetSection(DatabaseIdSettingKey).Value;
            if (string.IsNullOrEmpty(appSetting))
            {
                return -1;
            }

            int databaseId;
            if (!int.TryParse(appSetting, out databaseId))
            {
                return -1;
            }

            return databaseId;
        }

        private string GetDefaultConnectionString()
        {
            var connStr = this.Configuration.GetSection(ConnectionStringKey).Value;

            if (string.IsNullOrEmpty(connStr))
            {
                connStr = "127.0.0.1";
            }
            System.Console.WriteLine("RedisHost----------------------------------------------------------");
            System.Console.WriteLine(connStr);
            System.Console.WriteLine("RedisHost----------------------------------------------------------");
            return connStr;
        }
    }
}
