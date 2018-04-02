using AccountGRPCInterface;
using SP.Api.Cache;
using SP.Api.Model;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiGateway.Models.Oauth2;

namespace WebApiGateway.Models.Token
{
    class TokenRepository : ITokenRepository
    {
        private static string userOAuthTokenTime;
        private static object CommonTokenLock = new object();

        /// <summary>
        /// 初始化接口TOKEN集合缓存时间
        /// </summary>
        public TokenRepository()
        {
            userOAuthTokenTime = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// 重置缓存
        /// </summary>
        private void reset()
        {
            userOAuthTokenTime = DateTime.Now.ToShortDateString();
        }

        /// <summary>
        /// 获取token的用户信息
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        public TokenModel Get(string userKey)
        {
            var tokenModel = new TokenModel();

            var accessToken = ApiTokenCache.GetAccessToken(userKey);

            if (accessToken != null)
            {
                //if (accessToken.AccessTokenExpires >= DateTime.Now)
                //{
                return new TokenModel()
                {
                    Access_Token = accessToken.AccessToken,
                    AccountId = accessToken.AccountID,
                    //AppID = accessToken.AppID,
                    Access_Token_Expires = accessToken.AccessTokenExpires.ToString()
                };
                //}
                //else
                //    MD.Caching.ApiCache.RemoveAccessToken(accessToken.AccessToken);
            }

            //var accessUserInfo = MD.Business.SQL.OAuth2AccessToken.GetAccessToken(userKey);
            var accessUserInfo = AccountBusiness.GetAccessToken(userKey);

            if (accessUserInfo != null)
            {
                tokenModel.Access_Token = accessUserInfo.Access_Token;
                //tokenModel.CreateUser = accessUserInfo.AccountId;
                tokenModel.Access_Token_Expires = accessUserInfo.Access_Token_Expires;

                lock (CommonTokenLock)
                {
                    var entity = new OAuth2AccessToken()
                    {
                        AccessToken = accessUserInfo.Access_Token,
                        AccountID = accessUserInfo.AccountId,
                        AccessTokenExpires = DateTime.Parse(accessUserInfo.Access_Token_Expires),
                        RefreshToken = accessUserInfo.Refresh_Token,
                        //RefreshTokenExpires = DateTime.Parse(accessUserInfo.Refresh_Token_Expires)
                    };
                    ApiTokenCache.SetAccessToken(tokenModel.Access_Token, entity);
                }
            }

            return tokenModel;
        }

        /// <summary>
        /// refreshToken获取token详情
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        public TokenModel GetTokenByRefreshToken(string refreshToken)
        {
            var tokenModel = new TokenModel();

            var accessUserInfo = AccountBusiness.GetAccessTokenByRefreshToken(refreshToken);

            //从缓存提取访问令牌
            if (accessUserInfo != null)
            {
                tokenModel.Access_Token = accessUserInfo.Access_Token;
                tokenModel.AccountId = accessUserInfo.AccountId;
                //tokenModel.AppID = accessUserInfo.AppID;
                tokenModel.Access_Token_Expires = accessUserInfo.Access_Token_Expires;

                return tokenModel;
            }
            else
                return null;
        }

        /// <summary>
        /// 创建TOKEN并添加到缓存当中
        /// </summary>
        /// <param name="resultObj"></param>
        /// <param name="app"></param>
        /// <param name="projectId"></param>
        /// <param name="accountId"></param>
        public void Add(TokenModel accessTokenModel, string accountId)
        {
            DateTime dateNow = DateTime.Now;
            int accessTokenExpires = 48;
            int refreshTokenExpires = 52;
            
            string newAccessToken = System.Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
            DateTime newAccessTokenExpires = dateNow.AddHours(accessTokenExpires);
            string newRefreshToken = System.Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
            DateTime newRefreshTokenExpires = dateNow.AddHours(refreshTokenExpires);

            var entity = new OAuth2AccessToken()
            {
                AccessToken = newAccessToken,
                AccountID = accountId,                
                AccessTokenExpires = newAccessTokenExpires,
                RefreshToken = newRefreshToken,
                RefreshTokenExpires = newRefreshTokenExpires,
                CreateTime = dateNow
            };
            

            var token = AccountBusiness.AddAccessToken(entity);

            if (token)
            {
                ApiTokenCache.SetAccessToken(entity.AccessToken, entity);
                ApiTokenCache.SetTokenAccount(entity.AccessToken, entity.AccountID);

                accessTokenModel.Access_Token = entity.AccessToken;
                accessTokenModel.Access_Token_Expires = Convert.ToString(accessTokenExpires * 3600);
                accessTokenModel.Refresh_Token = entity.RefreshToken;
            }
        }

        public void Remove(string token, string accountId, string appId, string projectId)
        {
            AccountBusiness.RemoveAccessToken(token, accountId);
            ApiTokenCache.RemoveAccessToken(token);
        }
    }
}