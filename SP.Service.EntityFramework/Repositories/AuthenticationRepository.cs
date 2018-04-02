using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AuthenticationRepository : EfRepository<AuthenticationEntity>
    {
        public AuthenticationRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public bool Add(AuthenticationEntity item)
        {
            var result = this.Insert(item);
            return result > 0;
        }
        public bool UpdateAuthenticationStatus(AuthenticationEntity item)
        {
            var result = this.UpdateNonDefaults(item,x=>x.Account == item.Account && x.AuthType == item.AuthType && x.Status == 0);
            return result > 0;
        }

        public string GetValidVerifyCodeByAccount(string account, int authType,string accountId)
        {
            var verifyCode = string.Empty;

            var tempAuthList = this.Select(m => m.Account == account && m.AuthType == authType && m.Status == 0);

            if (tempAuthList.Count > 0)
            {
                var authList = tempAuthList.OrderByDescending(m => m.UpdateTime).ToList();

                verifyCode = authList[0].VerifyCode;
            }

            return verifyCode;
        }
    }
}
