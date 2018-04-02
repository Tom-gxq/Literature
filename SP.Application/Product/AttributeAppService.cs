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
    public class AttributeAppService : ApplicationService, IAttributeAppService
    {
        private readonly IRepository<AttributeEntity, int> _attributeRepository;
        public AttributeAppService(IRepository<AttributeEntity, int> attributeRepositor)
        {
            _attributeRepository = attributeRepositor;
        }

        public bool AddAttribute(AttributeDto attribute)
        {
            var result = _attributeRepository.Insert(new AttributeEntity()
            {
                AttributeName = attribute.AttributeName,
                DisplaySequence = attribute.DisplaySequence,
                UseAttributeImage = attribute.UseAttributeImage,
            });
            return result != null;
        }

        public bool UpdaeAttributeDisplaySequence(int attributeId, int displaySequence)
        {
            var reuslt = _attributeRepository.UpdateNonDefaults(new AttributeEntity()
            {
                DisplaySequence = displaySequence,
                Id = attributeId
            }, x => x.Id == attributeId);
            return reuslt > 0;
        }

        public bool DeleteAttribute(int attributeId)
        {
            var reuslt = false;
            try
            {
                var repository = IocManager.Instance.Resolve<AttributeRespository>();
                repository.DeleteAttribute(attributeId);
                reuslt = true;
            }
            catch
            {

            }
            return reuslt;
        }

        public int GetAttributeListCount()
        {
            var total = _attributeRepository.Count();
            return total;
        }
        public List<AttributeDto> GetAttributeList(int pageIndex, int pageSize)
        {
            var retList = new List<AttributeDto>();
            var repository = IocManager.Instance.Resolve<AttributeRespository>();
            var list = repository.GetAttributeList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }
        public int SearchAttributeByNameCount(string name)
        {
            var repository = IocManager.Instance.Resolve<AttributeRespository>();
            var total = repository.SearchAttributeByNameCount(name);

            return total;
        }
        public List<AttributeDto> SearchAttributeByName(string name, int pageIndex, int pageSize)
        {
            var retList = new List<AttributeDto>();
            var repository = IocManager.Instance.Resolve<AttributeRespository>();
            var list = repository.SearchAttributeByName(name, pageIndex, pageSize);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }
        public bool EditAttribute(AttributeDto attribute)
        {
            var result = _attributeRepository.UpdateNonDefaults(new AttributeEntity()
            {
                Id = attribute.Id,
                DisplaySequence = attribute.DisplaySequence,
                AttributeName = attribute.AttributeName
            }, x => x.Id == attribute.Id);
            return result > 0;
        }
        public AttributeDto GetAttributeDetail(int id)
        {
            var entity = _attributeRepository.Single(x => x.Id == id);
            return ConvertFromRepositoryEntity(entity);
        }

        private static AttributeDto ConvertFromRepositoryEntity(AttributeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var dto = new AttributeDto
            {
                Id = entity.Id,
                DisplaySequence = entity.DisplaySequence!=null?entity.DisplaySequence.Value:0,
                AttributeName = entity.AttributeName
            };

            return dto;
        }
    }
}
