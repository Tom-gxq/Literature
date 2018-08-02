using Lib.Application.Services;
using LibMain.Dependency;
using LibMain.Domain.Repositories;
using SP.Application.Product.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product
{
    public class ProductTypeService: ApplicationService,IProductTypeService
    {
        private readonly IRepository<ProductTypeEntity> _productTypeRepository;

        public ProductTypeService(IRepository<ProductTypeEntity> productTypeRepository)
        {
            _productTypeRepository = productTypeRepository;
        }

        public bool AddProductType(string typeName, int displaySequence,string typePath,string typeLogo, int kind)
        {
            var reuslt = _productTypeRepository.Insert(new ProductTypeEntity()
            {
                 TypeName = typeName,
                 DisplaySequence = displaySequence,
                 TypePath = typePath,
                 TypeLogo = typeLogo,
                 Kind = kind
            });
            return reuslt > 0;
        }

        public bool DelProductType(int id)
        {
            var reuslt = _productTypeRepository.Delete(x => x.Id == id);
            return reuslt > 0;
        }

        public List<ProductTypeDto> GetProductTypeList(int kind,int pageIndex, int pageSize)
        {
            var retList = new List<ProductTypeDto>();
            var repository = IocManager.Instance.Resolve<ProductTypeRespository>();
            var list = repository.GetProductTypeList(kind,pageIndex, pageSize);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }
        public List<ProductTypeDto> GetTypeList(string accountId, int pageIndex, int pageSize)
        {
            var retList = new List<ProductTypeDto>();
            var repository = IocManager.Instance.Resolve<ProductTypeRespository>();
            var list = repository.GetTypeList(accountId, pageIndex, pageSize);
            var typeList = list.GroupBy(x=>x.Id);
            foreach (var item in typeList)
            {
                var adminUser = ConvertFromRepositoryEntity(item.FirstOrDefault());
                retList.Add(adminUser);
            }
            return retList;
        }
        public List<ProductTypeDto> GetAllProductTypeList(int kind)
        {
            var retList = new List<ProductTypeDto>();
            var repository = IocManager.Instance.Resolve<ProductTypeRespository>();
            var list = repository.GetAllProductTypeList(kind);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }

        public List<ProductTypeDto> SearchProductTypeByName(string typeName, int pageIndex, int pageSize)
        {
            var retList = new List<ProductTypeDto>();
            var repository = IocManager.Instance.Resolve<ProductTypeRespository>();
            var list = repository.SearchProductTypeByName(typeName,pageIndex, pageSize);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }

        public bool EditProductType(ProductTypeDto productType)
        {
            var result = _productTypeRepository.UpdateNonDefaults(new ProductTypeEntity()
            {
                Id=productType.TypeId,
                DisplaySequence = productType.DisplaySequence,
                TypeName = productType.TypeName,
                Remark = productType.Remark,
                TypePath = productType.TypePath,
                TypeLogo = productType.TypeLogo
            },x=>x.Id == productType.TypeId);
            return result > 0;
        }

        public bool DeleteProductTypeById(int id)
        {
            var result = _productTypeRepository.Delete(x => x.Id == id);
            return result > 0;
        }

        public ProductTypeDto GetProductTypeDetail(int id)
        {
            var type = _productTypeRepository.Single(x => x.Id == id);
            return ConvertFromRepositoryEntity(type);
        }

        public int GetProductTypeListCount()
        {
            var repository = IocManager.Instance.Resolve<ProductTypeRespository>();
            var total = repository.GetProductTypeListCount();

            return total;
        }

        public int SearchProductTypeByNameCount(string typeName)
        {
            var repository = IocManager.Instance.Resolve<ProductTypeRespository>();
            var total = repository.SearchProductTypeByNameCount(typeName);

            return total;
        }

        private static ProductTypeDto ConvertFromRepositoryEntity(ProductTypeEntity productType)
        {
            if (productType == null)
            {
                return null;
            }
            var productsDto = new ProductTypeDto
            {
                TypeId = productType.Id,
                DisplaySequence = productType.DisplaySequence.Value,
                TypeName = productType.TypeName,
                Kind = productType.Kind != null ? productType.Kind.Value:0,
                Remark = productType.Remark,
                TypePath = productType.TypePath,
                TypeLogo = productType.TypeLogo,
            };

            return productsDto;
        }
    }
}
