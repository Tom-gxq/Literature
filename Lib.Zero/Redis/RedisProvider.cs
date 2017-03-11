using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Zero.Redis
{
    public class RedisProvider
    {
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
    }
}
