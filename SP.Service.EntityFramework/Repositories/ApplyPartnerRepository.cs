using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ApplyPartnerRepository : EfRepository<ApplyPartnerEntity>
    {
        public ApplyPartnerRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool Add(ApplyPartnerEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }
    }
}
