using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXApiGate.Common
{
    public class ConfigHelper
    {
        private static string redisMdServer;
        /// <summary>
        /// Redis 主站配置(host:port)
        /// </summary>
        public static string RedisMdServer
        {
            get
            {
                if (string.IsNullOrEmpty(redisMdServer))
                {
                    redisMdServer = ConfigurationManager.AppSettings["Redis.Servers"];
                }
                return redisMdServer;
            }
        }

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
                    redisSessionServer = ConfigurationManager.AppSettings["SessionRedis.Servers"];
                }
                return redisSessionServer;
            }
        }

        private static string redisMDApiServer;
        /// <summary>
        /// Redis MDAPI配置
        /// </summary>
        public static string RedisMDApiServer
        {
            get
            {
                if (string.IsNullOrEmpty(redisMDApiServer))
                {
                    redisMDApiServer = ConfigurationManager.AppSettings["ApiRedis.Servers"];
                }
                return redisMDApiServer;
            }
        }
        
    }
}
