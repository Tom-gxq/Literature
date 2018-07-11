using CSRedis;
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
        private const string SentinelKey = "Redis.Sentinel";
        private const string MasterRedisNameKey = "Redis.MasterRedisName";

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
            var connStr = this.Configuration.GetSection(SentinelKey)?.Value??string.Empty;
            using (var sentinel = new RedisSentinelClient(connStr, 26379))
            {
                var masterRedisName = this.Configuration.GetSection(MasterRedisNameKey)?.Value;
                sentinel.SentinelsAsync(masterRedisName);
                System.Console.WriteLine("sentinel:"+ sentinel.ToString());                
                var master = sentinel.Slaves(masterRedisName);//这个就是在Sentinel上面为Master主机起的名字，要一致 
                if(master != null && master.Length > 0)
                {
                    connStr =$"{master[0].MasterHost}:{master[0].MasterPort}" ;
                }
            }
            if (string.IsNullOrEmpty(connStr))
            {
                connStr = this.Configuration.GetSection(ConnectionStringKey).Value;
            }

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
