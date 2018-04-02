using SP.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Cache
{
    public class ApiTokenCache
    {
        private static readonly string redisKeyApiCode = RedisKeys.ApiCodeKey;
        private static readonly string redisKeyApiAuth = RedisKeys.ApiAuthKey;
        private static readonly string redisKeyApiRequest = RedisKeys.ApiRequestKey;

        private static readonly string redisKeyToken = RedisKeys.ApiTokenKey;
        private static readonly string redisKeyVerify = RedisKeys.ApiVerifyKey;
        private static readonly string redisKeyAccessToken = RedisKeys.ApiAccessTokenKey;
        #region api授权部分

        #region 临时访问令牌
        public static bool SetAppAccountCode(string code, string codeParam)
        {
            var key = string.Format("{0}{1}", redisKeyApiCode, code);

            bool flag = RedisProvider.MDAPI.Set(key, codeParam);
            RedisProvider.MDAPI.ExpireEntryAt(key, DateTime.Now.AddDays(1));

            return flag;
        }

        public static string GetAppAccountCode(string code)
        {
            var key = string.Format("{0}{1}", redisKeyApiCode, code);

            var result = RedisProvider.MDAPI.Get<string>(key);
            RedisProvider.MDAPI.Remove(key);

            return result;
        }
        #endregion

        #region 是否授权
        /// <summary>
        /// 设置是否授权
        /// </summary>
        /// <param name="key">key为accountid和projectid的组合</param>
        /// <param name="appId"></param>
        /// <param name="isAuth"></param>
        /// <returns></returns>
        public static bool SetIsAuthorization(string key, string appId, bool isAuth)
        {
            key = string.Format("{0}{1}", redisKeyApiAuth, key);
            return RedisProvider.MDAPI.SetEntryInHash(key, appId, isAuth);
        }

        public static bool GetIsAuthorization(string key, string appId)
        {
            key = string.Format("{0}{1}", redisKeyApiAuth, key);
            return RedisProvider.MDAPI.GetValueFromHash<bool>(key, appId);
        }
        #endregion

        #region 访问次数
        public static int SetTokenRequestCount(string token, string requestUrl)
        {
            var key = string.Format("{0}{1}", redisKeyApiRequest, token);

            var flag = false;
            var count = 0;

            if (!RedisProvider.MDAPI.ExistsKey(key))
                flag = true;
            else
                count = RedisProvider.MDAPI.GetValueFromHash<int>(key, requestUrl);

            count++;

            RedisProvider.MDAPI.SetEntryInHash(key, requestUrl, count);

            if (flag)
            {
                RedisProvider.MDAPI.ExpireEntryIn(key, TimeSpan.FromDays(1));
            }

            return count;
        }

        public static int GetTokenRequestCount(string token, string reqeustUrl)
        {
            var key = string.Format("{0}{1}", redisKeyApiRequest, token);
            return RedisProvider.MDAPI.GetValueFromHash<int>(key, reqeustUrl);
        }

        #endregion

        #region token缓存
        public static bool SetAccessToken(string token, OAuth2AccessToken tokenModel)
        {
            var tokenObject = new
            {
                AccessToken = tokenModel.AccessToken,
                AccountID = tokenModel.AccountID,
                AppID = tokenModel.AppID,
                ProjectID = tokenModel.ProjectID,
                AccessTokenExpires = tokenModel.AccessTokenExpires,
                RefreshToken = tokenModel.RefreshToken,
                RefreshTokenExpires = tokenModel.RefreshTokenExpires,
                CreateTime = tokenModel.CreateTime
            };

            return RedisProvider.MDAPI.SetEntryInHash(redisKeyAccessToken, token, tokenObject);
        }

        public static OAuth2AccessToken GetAccessToken(string token)
        {
            return RedisProvider.MDAPI.GetValueFromHash<OAuth2AccessToken>(redisKeyAccessToken, token);
        }

        public static bool RemoveAccessToken(string token)
        {
            return RedisProvider.MDAPI.RemoveEntryFromHash(redisKeyAccessToken, token);
        }

        /// <summary>
        /// token对应的accountid
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static bool SetTokenAccount(string token, string accountId)
        {
            var key = string.Format("{0}{1}", redisKeyToken, token);

            return RedisProvider.MDAPI.Set(key, accountId);
        }


        public static string GetTokenAccount(string token)
        {
            var key = string.Format("{0}{1}", redisKeyToken, token);
            return RedisProvider.MDAPI.Get<string>(key);
        }

        /// <summary>
        /// 登出移除
        /// </summary>
        /// <param name="token"></param>
        /// <param name="accountid"></param>
        /// <returns></returns>
        public static bool RemoveTokenAccount(string token, string accountid)
        {
            var key = string.Format("{0}{1}", redisKeyToken, token);

            return RedisProvider.MDAPI.Remove(key);
        }
        #endregion

        #region 图片验证码
        public static bool SetAccountVerifyCode(string accountId, string verifyCode)
        {
            var key = string.Format("{0}{1}", redisKeyVerify, accountId);

            RedisProvider.MDAPI.Set(key, verifyCode);
            RedisProvider.MDAPI.ExpireEntryAt(key, DateTime.Now.AddDays(1));

            return true;
        }

        public static string GetAccountVerifyCode(string accountId)
        {
            var key = string.Format("{0}{1}", redisKeyVerify, accountId);
            var result = RedisProvider.MDAPI.Get<string>(key);

            return result;
        }
        #endregion
        #endregion
    }
}
