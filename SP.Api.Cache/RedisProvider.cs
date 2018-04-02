using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Cache
{
    public class RedisProvider
    {
        private static RedisHelper mdRedisHelper = null;
        /// <summary>
        /// WWW Redis
        /// </summary>
        public static RedisHelper Default
        {
            get
            {
                if (mdRedisHelper == null)
                {
                    mdRedisHelper = new RedisHelper(ConfigHelper.RedisMdServer);
                }
                return mdRedisHelper;
            }
        }

        private static RedisHelper sessionRedisHelper = null;
        /// <summary>
        /// Session Redis
        /// </summary>
        public static RedisHelper Session
        {
            get
            {
                if (sessionRedisHelper == null)
                {
                    sessionRedisHelper = new RedisHelper(ConfigHelper.RedisSessionServer);
                }
                return sessionRedisHelper;
            }
        }
        private static RedisHelper mdapiRedisHelper = null;
        /// <summary>
        /// MDAPI Redis(Application)
        /// </summary>
        public static RedisHelper MDAPI
        {
            get
            {
                if (mdapiRedisHelper == null)
                {
                    mdapiRedisHelper = new RedisHelper(ConfigHelper.RedisMDApiServer);
                }
                return mdapiRedisHelper;
            }
        }
        

    }
}
