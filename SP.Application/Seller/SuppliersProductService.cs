using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMain.Dependency;
using SP.Application.Seller.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;

namespace SP.Application.Seller
{
    public class SuppliersProductService : ISuppliersProductService
    {
        public bool AddProduct(SuppliersProductDto data)
        {
            var repository = IocManager.Instance.Resolve<SuppliersProductRepository>();
            return repository.Add(new SuppliersProductEntity()
            {
                SuppliersId = data.SuppliersId,
                ProductId = data.ProductId,
                AlertStock = data.AlertStock,
                PurchasePrice = data.PurchasePrice,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            });
        }

        public bool DelProduct(int id)
        {
            return true;
        }

        public List<SuppliersProductDto> GetSuppliersProductList(int pageIndex, int pageSize, int sellerId)
        {
            var retList = new List<SuppliersProductDto>();
            var repository = IocManager.Instance.Resolve<SuppliersProductRepository>();
            var list = repository.GetProductList(pageIndex, pageSize, sellerId);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }

        private static SuppliersProductDto ConvertFromRepositoryEntity(SuppliersProductEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var supplerRepos = IocManager.Instance.Resolve<SuppliersProductRepository>();

            var supplierProductDto = new SuppliersProductDto
            {
                Id = entity.Id,
                SuppliersId = entity.SuppliersId,
                ProductId = entity?.ProductId,
                PurchasePrice = entity.PurchasePrice,
                Status = entity.Status,
                AlertStock = entity.AlertStock
            };

            return supplierProductDto;
        }

        public bool UpdateProduct(SuppliersProductDto dto)
        {
            throw new NotImplementedException();
        }

        public int GetSuppliersProductCount(int sellerId)
        {
            var supplerRepos = IocManager.Instance.Resolve<SuppliersProductRepository>();
            return supplerRepos.GetSuppliersProductCount(sellerId);
        }
    }
}
