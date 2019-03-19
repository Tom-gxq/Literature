using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class RepeatedTokenRespository : EfRepository<RepeatedTokenEntity>
    {
        public RepeatedTokenRespository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddToken(RepeatedTokenEntity token)
        {
            var result = this.Insert(token);
            return result > 0;
        }

        public RepeatedTokenEntity GetTokenByKey(string accountId,string userKey)
        {
            return this.Single(x => x.AccessToken == userKey && x.AccountID == accountId);
        }

        public bool UpdateToken(RepeatedTokenEntity token)
        {
            var result = this.UpdateNonDefaults(token, x => x.AccessToken == token.AccessToken && x.AccountID==token.AccountID);
            return result > 0;
        }
    }
}
