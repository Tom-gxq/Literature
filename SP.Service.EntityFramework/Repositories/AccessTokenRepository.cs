using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AccessTokenRepository : EfRepository<OAuth2AccessToken>
    {
        public AccessTokenRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddAccessToken(OAuth2AccessToken account)
        {
            var result = this.Insert(account);
            return result > 0;
        }

        public OAuth2AccessToken GetAccessTokenByKey(string userKey)
        {
            return this.Single(x => x.AccessToken == userKey);
        }

        public OAuth2AccessToken GetAccessTokenByRefreshToken(string userKey)
        {
            return this.Single(x => x.RefreshToken == userKey);
        }

        public bool RemoveAccessToken(string userKey, string accountId)
        {
            var result = this.Delete(x=>x.AccessToken == userKey && x.AccountId == accountId);
            return result > 0;
        }
    }
}
