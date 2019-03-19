using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SuppliersProductRepository : EfRepository<SuppliersProductEntity>
    {
        public SuppliersProductRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool AddSuppliers(SuppliersProductEntity suppliers)
        {
            var result = this.Insert(suppliers);
            return result > 0;
        }
    }
}
