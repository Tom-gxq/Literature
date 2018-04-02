using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ProductTypeRepository : EfRepository<ProductTypeEntity>
    {
        public ProductTypeRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public ProductTypeEntity GetProductTypeById(int typeId)
        {
            var result = this.Single(x => x.Id == typeId);
            return result;
        }
    }
}
