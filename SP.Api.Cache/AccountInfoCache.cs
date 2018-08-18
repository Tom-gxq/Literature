using AccountGRPCInterface;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Cache
{
    public class AccountInfoCache
    {
        private static readonly string redisKey = RedisKeys.AccountInfoKey;

        /// <summary>
        /// 获取账户详情(从redis中)
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="projectId"></param>
        /// <returns>成功返回账户信息，失败返回null</returns>
        public static AccountModel GetAccountInfoByAccountId(string accountId)
        {
            if (string.IsNullOrEmpty(accountId)) return null;
            

            var accountInfo = RedisProvider.Default.GetValueFromHash<AccountModel>(redisKey, accountId);
            //redis中存在，从redis中获取
            if (accountInfo == null && !string.IsNullOrEmpty(accountId))
            {
                accountInfo = ReloadAccountInfo(accountId);
            }
            
            return accountInfo;
        }
        /// <summary>
        /// 重新获取账户详情到redis中
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public static AccountModel ReloadAccountInfo(string accountId)
        {
            AccountModel accountInfo = null;
            //AccountInfo.GetAccountInfoByAccountId(accountId);
            accountInfo = AccountBusiness.GetAccountDetail(accountId);
            if (accountInfo != null)
                RedisProvider.Default.SetEntryInHash(redisKey, accountId, GetCacheObject(accountInfo));

            return accountInfo;
        }
        private static object GetCacheObject(AccountModel accountInfo)
        {
            return new
            {
                accountInfo.AccountId,
                accountInfo.MobilePhone,
                accountInfo.UserType
            };
        }
    }
}
