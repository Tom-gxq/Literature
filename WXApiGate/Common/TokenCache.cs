using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WXApiGate.Model;

namespace WXApiGate.Common
{
    public class TokenCache
    {
        static ITokenRepository iTokenRepository;

        static TokenCache()
        {
            iTokenRepository = new TokenRepository();
        }

        public static TokenModel Get(string userKey)
        {
            return iTokenRepository.Get(userKey);
        }

        public static TokenModel GetTokenByRefreshToken(string refreshToken)
        {
            return iTokenRepository.GetTokenByRefreshToken(refreshToken);
        }

        public static void Add(TokenModel accessTokenModel, string accountId)
        {
            iTokenRepository.Add(accessTokenModel, accountId);
        }

        public static void Remove(string token, string accountId, string appId, string projectId)
        {
            iTokenRepository.Remove(token, accountId, appId, projectId);
        }
    }
}