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
    public class BrandAppService : ApplicationService, IBrandAppService
    {
        private readonly IRepository<BrandEntity, int> _brandRepository;
        public BrandAppService(IRepository<BrandEntity, int> brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public bool AddBrand(BrandDto brand)
        {
            var result = _brandRepository.Insert(new BrandEntity()
            {
                BrandName = brand.BrandName,
                CompanyUrl = brand.CompanyUrl,
                Description = brand.Description,
                DisplaySequence = brand.DisplaySequence,
                Logo = brand.Logo,
                MetaKeywords= $"{brand.BrandName}|{brand.CompanyUrl}",
            });
            return result != null;
        }

        public bool UpdateBrandDisplaySequence(int brandId, int displaySequence)
        {
            var reuslt = _brandRepository.UpdateNonDefaults(new BrandEntity()
            {
                DisplaySequence = displaySequence,
                Id = brandId
            },x=>x.Id == brandId);
            return reuslt > 0;
        }

        public bool DeleteBrand(int brandId)
        {
            var repository = IocManager.Instance.Resolve<BrandRespository>();
            return repository.DeleteBrand(brandId);
        }

        public List<BrandDto> GetBrandList(int pageIndex, int pageSize)
        {
            var retList = new List<BrandDto>();
            var repository = IocManager.Instance.Resolve<BrandRespository>();
            var list = repository.GetBrandList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }
            
            return retList;
        }
        public List<BrandDto> GetAllBrandList()
        {
            var retList = new List<BrandDto>();
            var repository = IocManager.Instance.Resolve<BrandRespository>();
            var list = repository.GetAllBrandList();
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }
        public int GetBrandListCount()
        {
            var total = _brandRepository.Count();
            return total;
        }
        public int SearchBrandByNameCount(string brandName)
        {
            var repository = IocManager.Instance.Resolve<BrandRespository>();
            var total = repository.SearchBrandByNameCount(brandName);
            
            return total;
        }
        public List<BrandDto> SearchBrandByName(string brandName, int pageIndex, int pageSize)
        {
            var retList = new List<BrandDto>();
            var repository = IocManager.Instance.Resolve<BrandRespository>();
            var list = repository.SearchBrandByName(brandName, pageIndex, pageSize);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }
        public bool EditBrand(BrandDto brand)
        {
            var result = _brandRepository.UpdateNonDefaults(new BrandEntity()
            {
                Id = brand.BrandId,
                DisplaySequence = brand.DisplaySequence,
                BrandName = brand.BrandName,
                Description = brand.Description
            }, x => x.Id == brand.BrandId);
            return result > 0;
        }
        public BrandDto GetBrandDetail(int id)
        {
            var type = _brandRepository.Single(x => x.Id == id);
            return ConvertFromRepositoryEntity(type);
        }

        private static BrandDto ConvertFromRepositoryEntity(BrandEntity brand)
        {
            if (brand == null)
            {
                return null;
            }
            var brandDto = new BrandDto
            {
                BrandId = brand.Id,
                DisplaySequence = brand.DisplaySequence.Value,
                BrandName = brand.BrandName,
                Description = brand.Description,
                Logo = brand.Logo
            };

            return brandDto;
        }
    }
}
