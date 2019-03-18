using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using LibMain.Dependency;
using SP.Application.Seller.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;

namespace SP.Application.Seller
{
    public class SuppliersRegionService : ISuppliersRegionService
    {
        public bool AddSeller(SuppliersRegionDto dto)
        {
            var repository = IocManager.Instance.Resolve<SuppliersRegionRespository>();
            return repository.Add(new SuppliersRegionEntity()
            {
                SuppliersId = dto.SuppliersId,
                RegionID = dto.RegionID,
                CreateTime = DateTime.Now
            });
        }

        public bool DelSeller(int id)
        {
            var repository = IocManager.Instance.Resolve<SuppliersRegionRespository>();
            return repository.DelRegion(id);
        }

        List<SuppliersRegionDto> ISuppliersRegionService.GetSuppliersRegionList(int pageIndex, int pageSize)
        {
            var retList = new List<SuppliersRegionDto>();
            var repository = IocManager.Instance.Resolve<SuppliersRegionRespository>();
            var list = repository.GetSuppliersRegionList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }

        public long GetSuppliersRegionCount()
        {
            var repository = IocManager.Instance.Resolve<SuppliersRegionRespository>();
            var count = repository.Count();
            return count;
        }

        private static SuppliersRegionDto ConvertFromRepositoryEntity(SuppliersRegionEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var supplerRepos = IocManager.Instance.Resolve<SupplersRepository>();
            var supplerInfo = supplerRepos.GetSupplerById((int)entity.SuppliersId);
            var regionRepos = IocManager.Instance.Resolve<RegionRespository>();
            var regionInfo = regionRepos.GetRegionDataDetail((int)entity.RegionID);

            var supplierRegionDto = new SuppliersRegionDto
            {
                Id = (int)entity?.Id,
                SuppliersId = (int)entity?.SuppliersId,
                SuppliersName = supplerInfo?.SuppliersName,
                RegionID = (int)entity?.RegionID,
                RegionName = regionInfo?.DataName
            };

            return supplierRegionDto;
        }

        public SuppliersRegionDto GetSupplerDetail(int id)
        {
            var repository = IocManager.Instance.Resolve<SuppliersRegionRespository>();
            var entity = repository.GetSupplierRegionById(id);

            var supplerRepos = IocManager.Instance.Resolve<SupplersRepository>();
            var supplerInfo = supplerRepos.GetSupplerById((int)entity.SuppliersId);
            var regionRepos = IocManager.Instance.Resolve<RegionRespository>();
            var regionInfo = regionRepos.GetRegionDataDetail((int)entity.RegionID);

            var supplierRegionDto = new SuppliersRegionDto
            {
                Id = (int)entity?.Id,
                SuppliersId = (int)entity?.SuppliersId,
                SuppliersName = supplerInfo?.SuppliersName,
                RegionID = (int)entity?.RegionID,
                RegionName = regionInfo?.DataName
            };

            return supplierRegionDto;
        }

        bool ISuppliersRegionService.UpdateSeller(SuppliersRegionDto dto)
        {
            var repository = IocManager.Instance.Resolve<SuppliersRegionRespository>();
            return repository.EditRegion(new SuppliersRegionEntity()
            {
                Id = dto.Id,
                SuppliersId = dto.SuppliersId,
                RegionID = dto.RegionID,
                UpdateTime = DateTime.Now
            });
        }

        public List<SuppliersRegionDto> SearchRegionByName(string supplierName, int pageIndex, int pageSize)
        {
            var retList = new List<SuppliersRegionDto>();
            var repository = IocManager.Instance.Resolve<SuppliersRegionRespository>();
            var list = repository.SearchRegionByName(supplierName, pageIndex, pageSize);
            foreach (var supplier in list)
            {
                var supplierDto = ConvertFromRepositoryEntity(supplier);
                retList.Add(supplierDto);
            }
            return retList;
        }

        public int SearchRegionByNameCount(string supplierName)
        {
            var repository = IocManager.Instance.Resolve<SuppliersRegionRespository>();
            var total = repository.SearchRegionByNameCount(supplierName);
            return total;
        }
    }
}
