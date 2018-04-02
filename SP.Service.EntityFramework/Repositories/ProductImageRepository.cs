using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class ProductImageRepository : EfRepository<ProductImageEntity>
    {
        public ProductImageRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public List<ProductImageEntity> GetProductImageByProductId(string productId)
        {
            var result = this.Select(x => x.ProductId == productId);
            return result;
        }
    }
}
