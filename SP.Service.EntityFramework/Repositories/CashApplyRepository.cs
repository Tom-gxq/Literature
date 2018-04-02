using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class CashApplyRepository : EfRepository<CashApplyEntity>
    {
        public CashApplyRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool Add(CashApplyEntity account)
        {
            var result = this.Insert(account);
            return result > 0;
        }
    }
}
