using SP.Api.Model.RepeatedToken;
using SP.Service;
using System;
using System.Collections.Generic;

namespace RepeatedTokenGRPCInterface
{
    public class RepeatedTokenBusiness
    {
        public static RepeatedTokenModel GetRepeatedTokenByKey(string tokenKey, string accountId)
        {
            var client = RepeatedTokenClientHelper.GetClient();
            var request1 = new TokenKeyRequest()
            {
                Key = tokenKey,
                AccountId = accountId
            };
            var result = client.GetRepeatedToken(request1);
            var domain = new RepeatedTokenModel();
            if (result.Status == 10001)
            {
                var item = result.RepeatedToken;                
                domain.Access_Token = item.AccessToken;
                domain.AccountId = item.AccountId;                
            }
            return domain;
        }
        public static bool AddRepeatedToken(string token,string accountId)
        {
            var client = RepeatedTokenClientHelper.GetClient();
            var request1 = new RepeatedTokenRequest()
            {
                AccessToken = new RepeatedToken()
                {
                    AccessToken = token,
                    AccountId = accountId,
                    CreateTime = DateTime.Now.Ticks,
                    Status = 0
                }
            };
            var result = client.AddRepeatedToken(request1);
            
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdateRepeatedTokenDisabled(string tokenKey, string accountId)
        {
            var client = RepeatedTokenClientHelper.GetClient();
            var request1 = new TokenKeyRequest()
            {
                Key= tokenKey,
                AccountId = accountId
            };
            var result = client.UpdateRepeatedTokenDisabled(request1);
            
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
