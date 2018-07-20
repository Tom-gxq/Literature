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

        public bool Add(ProductImageEntity image)
        {
            var result = this.Insert(image);
            return result > 0;
        }

        public int Update(ProductImageEntity product)
        {
            using (var db = OpenDbConnection())
            {
                return this.UpdateNonDefaults(product, x => x.ProductId == product.ProductId
                && x.Postion == product.Postion && x.DisplaySequence == product.DisplaySequence);
            }
        }
        public ProductImageEntity GetProductImageById(string productId)
        {
            var result = this.Single(x => x.ProductId == productId && x.Postion==0 &&x.DisplaySequence==1);
            return result;
        }
    }
}
