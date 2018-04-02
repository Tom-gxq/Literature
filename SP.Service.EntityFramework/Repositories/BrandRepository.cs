using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class BrandRepository : EfRepository<BrandEntity>
    {
        public BrandRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public BrandEntity GetBrandById(int brandId)
        {
            var result = this.Single(x => x.Id == brandId);
            return result;
        }
    }
}
