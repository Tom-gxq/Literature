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
    public class RegionAppService : ApplicationService, IRegionAppService
    {
        private readonly IRepository<RegionEntity, int> _regionRepository;
        public RegionAppService(IRepository<RegionEntity, int> regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public List<RegionDto> GetRegionData(int dataType)
        {
            var retList = new List<RegionDto>();
            var repository = IocManager.Instance.Resolve<RegionRespository>();
            var list = repository.GetRegionData(dataType);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }
        public List<RegionDto> SearchRegionData(string dataName)
        {
            var retList = new List<RegionDto>();
            var repository = IocManager.Instance.Resolve<RegionRespository>();
            var list = repository.SearchRegionData(dataName);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }
        public long GetRegionDataCount(int dataType)
        {
            var retList = new List<RegionDto>();
            var repository = IocManager.Instance.Resolve<RegionRespository>();
            var count = repository.Count(x=>x.DataType == dataType);
            return count;
        }
        public List<RegionDto> GetChildRegionData(int parentId)
        {
            var retList = new List<RegionDto>();
            if (parentId > 0)
            {
                var repository = IocManager.Instance.Resolve<RegionRespository>();
                var list = repository.GetChildRegionData(parentId);
                foreach (var item in list)
                {
                    var entity = ConvertFromRepositoryEntity(item);
                    retList.Add(entity);
                }
            }

            return retList;
        }
        public List<RegionDto> GetChildRegionData(int parentId, int pageIndex, int pageSize)
        {
            var retList = new List<RegionDto>();
            if (parentId > 0)
            {
                var repository = IocManager.Instance.Resolve<RegionRespository>();
                var list = repository.GetChildRegionData(parentId, pageIndex, pageSize);
                foreach (var item in list)
                {
                    var entity = ConvertFromRepositoryEntity(item);
                    retList.Add(entity);
                }
            }

            return retList;
        }
        public long GetChildRegionDataCount(int parentId)
        {
            long count = 0;
            if (parentId > 0)
            {
                var repository = IocManager.Instance.Resolve<RegionRespository>();
                count = repository.GetChildRegionDataCount(parentId);                
            }

            return count;
        }

        public bool AddRegionData(RegionDto region)
        {
            var repository = IocManager.Instance.Resolve<RegionRespository>();
            var reulst = repository.AddRegionData(new RegionEntity()
            {
                 DataType = region.DataType,
                 DataName = region.DataName,
                 Status = 1,
                 ParentDataID = region.ParentDataID,
                 CreateTime = DateTime.Now,
                 UpdateTime = DateTime.Now
            });
            return reulst;
        }

        public bool DelRegionData(int dataId)
        {
            var repository = IocManager.Instance.Resolve<RegionRespository>();
            var reulst = repository.DelRegionData(dataId);
            return reulst;
        }
        public RegionDto GetRegionDataDetail(int dataId)
        {
            if (dataId > 0)
            {
                var repository = IocManager.Instance.Resolve<RegionRespository>();
                var item = repository.GetRegionDataDetail(dataId);
                var entity = ConvertFromRepositoryEntity(item);
                if (entity != null && entity.ParentDataID > 0)
                {
                    var parent = repository.GetRegionDataDetail(entity.ParentDataID);
                    if (parent != null && parent.DataName != null)
                    {
                        entity.ParentDataName = parent.DataName;
                        
                        if (parent.ParentDataID != null && parent.ParentDataID.Value > 0)
                        {
                            var district = repository.GetRegionDataDetail(parent.ParentDataID.Value);
                            if (district != null && district.DataName != null)
                            {
                                entity.DistrictID = district.Id;
                                entity.DistrictName = district.DataName;
                            }
                        }
                    }
                }
                return entity;
            }
            else
            {
                return null;
            }
        }
        public RegionDto GetRegionData(int parentId, string dataName)
        {
            var repository = IocManager.Instance.Resolve<RegionRespository>();
            var entity = repository.GetRegionData(parentId, dataName);
            return ConvertFromRepositoryEntity(entity);
        }
        public bool EditRegionData(RegionDto region)
        {
            var repository = IocManager.Instance.Resolve<RegionRespository>();
            return repository.EditRegionData(new RegionEntity()
            {
                Id = region.DataId,
                DataType = region.DataType,
                DataName = region.DataName,
                ParentDataID = region.ParentDataID,
                UpdateTime = DateTime.Now
            });
        }
        public List<RegionTypeDto> GetRegionTypeList(int pageIndex, int pageSize)
        {
            var retList = new List<RegionTypeDto>();
            var repository = IocManager.Instance.Resolve<RegionTypeRespository>();
            var list = repository.GetRegionTypeList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }
        public int GetRegionTypeCount()
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<RegionTypeRespository>();
            var total = repository.GetRegionTypeCount();
            return total;
        }
        public bool AddRegionType(RegionTypeDto region)
        {
            var repository = IocManager.Instance.Resolve<RegionTypeRespository>();
            var reulst = repository.AddRegionType(new RegionTypeEntity()
            {
                TypeId = region.TypeId,
                RegionId = region.RegionId,
                DisplaySequence = region.DisplaySequence
            });
            return reulst;
        }

        public bool DelRegionType(int id)
        {
            var repository = IocManager.Instance.Resolve<RegionTypeRespository>();
            var reulst = repository.DelRegionType(id);
            return reulst;
        }

        public List<RegionTypeDto> SearchRegionTypeByKeyWord(string keywords)
        {
            var retList = new List<RegionTypeDto>();
            var repository = IocManager.Instance.Resolve<RegionTypeRespository>();
            var list = repository.SearchRegionTypeByKeyWord(keywords);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }

        private static RegionDto ConvertFromRepositoryEntity(RegionEntity region)
        {
            if (region == null)
            {
                return null;
            }
            var regionDto = new RegionDto
            {
                DataId = region.Id,
                DataName = region.DataName,
                DataType = region.DataType.Value,
                Status = region.Status.Value,
                ParentDataID = region.ParentDataID!= null ?region.ParentDataID.Value:0,
                ParentDataName = string.Empty,
                CreateTime = region.CreateTime != null ? region.CreateTime.Value: DateTime.MinValue,
                UpdateTime = region.UpdateTime != null ? region.UpdateTime.Value : DateTime.MinValue,                
            };
            if (regionDto.ParentDataID > 0)
            {
                var repository = IocManager.Instance.Resolve<RegionRespository>();
                var entity = repository.GetRegionDataDetail(regionDto.ParentDataID);
                regionDto.ParentDataName = entity?.DataName??string.Empty;
            }

            return regionDto;
        }
        private static RegionTypeDto ConvertFromRepositoryEntity(RegionTypeFullEntity region)
        {
            if (region == null)
            {
                return null;
            }
            var regionDto = new RegionTypeDto
            {
                Id = region.Id,
                DataName = region.DataName,
                TypeName = region.TypeName,
                RegionId = region.RegionId.Value,
                TypeId  = region.TypeId.Value,    
                DisplaySequence = region.DisplaySequence!= null ? region.DisplaySequence.Value:0
            };

            return regionDto;
        }

    }
}
