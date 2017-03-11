using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Zero.Redis
{
    class ConfigHelper
    {
        private static string redisSessionServer;
        /// <summary>
        /// Redis 会话主站配置(host:port)
        /// </summary>
        public static string RedisSessionServer
        {
            get
            {
                if (string.IsNullOrEmpty(redisSessionServer))
                {
                    redisSessionServer = ConfigurationManager.AppSettings["Lib.SessionRedis.Servers"];
                }
                return redisSessionServer;
            }
        }

        private static string domain;
        public static string ServerDomain
        {
            get
            {
                if(string.IsNullOrEmpty(domain))
                {
                    domain = ConfigurationManager.AppSettings["Domain.Server"];
                }
                return domain;
            }
        }
    }
}
