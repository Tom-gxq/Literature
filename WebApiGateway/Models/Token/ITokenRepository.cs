using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiGateway.Models.Oauth2;

namespace WebApiGateway.Models.Token
{
    interface ITokenRepository
    {
        TokenModel Get(string userKey);

        TokenModel GetTokenByRefreshToken(string refreshToken);

        void Add(TokenModel accessTokenModel, string userID);

        void Remove(string token, string accountId, string appId, string projectId);
    }
}
