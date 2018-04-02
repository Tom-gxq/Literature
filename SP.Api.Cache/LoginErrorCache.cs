using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Cache
{
    public class LoginErrorCache
    {
        private static readonly string redisKeyApiLogin = RedisKeys.LoginErrKey;

        public static bool Set(string key, object value, DateTime? expires = null)
        {
            var tkey = string.Format("{0}{1}", redisKeyApiLogin, key);
            return RedisProvider.MDAPI.Set(tkey, value, expires!= null ? (expires.Value - DateTime.Now): (DateTime.Now- DateTime.Now));
        }

        public static object Get(string key)
        {
            var tkey = string.Format("{0}{1}", redisKeyApiLogin, key);
            return RedisProvider.MDAPI.Get<int>(tkey);
        }

        public static bool Delete(string key)
        {
            var tkey = string.Format("{0}{1}", redisKeyApiLogin, key);
            return RedisProvider.MDAPI.Remove(tkey);
        }
    }
}
