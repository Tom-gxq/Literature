using Lib.Zero.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Zero.Runtime.Session
{
    public class DataSession
    {
        private static readonly string redisKey = RedisKeys.SessionHashKey; //"H:A:S:I:"; //hash account session info
        private static readonly string accountRedisKey = RedisKeys.SessionAccountHashKey;

        #region 公共对象会话

        private string GetSessionID()
        {
            string flag = string.Empty;

            flag = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Guid.NewGuid();

            flag = StringMD5.GetMd5Str32(flag);

            return flag;
        }

        /// <summary>
        /// 设置会话
        /// </summary>
        public string SetSession(string sessionID, string key, object obj, string accountid, DateTime time)
        {
            if (string.IsNullOrEmpty(sessionID))
                sessionID = GetSessionID();
            var sessionKey = redisKey + sessionID;

            RedisProvider.Session.SetEntryInHash<object>(sessionKey, key, obj);
            RedisProvider.Session.ExpireEntryAt(sessionKey, time);

            if (!string.IsNullOrEmpty(accountid))
            {
                var accountKey = accountRedisKey + accountid;
                RedisProvider.Session.Set<string>(accountKey, sessionID.ToLower());
                RedisProvider.Session.ExpireEntryAt(accountKey, time);
            }

            return sessionID;
        }

        /// <summary>
        /// 删除会话
        /// </summary>
        public bool DeleteSession(string sessionID, string key)
        {
            bool flag = false;
            var sessionKey = redisKey + sessionID;
            flag = RedisProvider.Session.RemoveEntryFromHash(sessionKey, key);
            return flag;
        }

        /// <summary>
        /// 删除会话
        /// </summary>
        public bool DeleteAllSession(string sessionID)
        {
            bool flag = false;
            var sessionKey = redisKey + sessionID;
            flag = RedisProvider.Session.Remove(sessionKey);
            return flag;
        }

        /// <summary>
        /// 获取会话
        /// </summary>
        public object GetSession(string sessionID, string key)
        {
            var sessionKey = redisKey + sessionID;
            return RedisProvider.Session.GetValueFromHash<object>(sessionKey, key);
        }

        public string GetStringSession(string sessionID, string key)
        {
            var sessionKey = redisKey + sessionID;
            return RedisProvider.Session.GetStringValueFromHash(sessionKey, key);
        }


        #endregion
    }
}
