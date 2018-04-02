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
    public class AttributeValueAppService : ApplicationService, IAttributeValueAppService
    {
        private readonly IRepository<AttributeValueEntity, int> _attributeValueRepository;
        public AttributeValueAppService(IRepository<AttributeValueEntity, int> attributeValueRepository)
        {
            _attributeValueRepository = attributeValueRepository;
        }

        public bool AddAttributeValue(AttributeValueDto attributeValue)
        {
            var result = _attributeValueRepository.Insert(new AttributeValueEntity()
            {
                AttributeId = attributeValue.AttributeId,
                ValueStr = attributeValue.ValueStr,
                DisplaySequence = attributeValue.DisplaySequence,
                ImageUrl = attributeValue.ImageUrl,                
            });
            return result != null;
        }

        public bool UpdateAttributeValueDisplaySequence(int attributeId, int displaySequence)
        {
            var reuslt = _attributeValueRepository.UpdateNonDefaults(new AttributeValueEntity()
            {
                DisplaySequence = displaySequence,
                AttributeId  = attributeId
            }, x => x.AttributeId == attributeId);
            return reuslt > 0;
        }

        public bool DeleteAttributeValue(int valueId)
        {
            var repository = IocManager.Instance.Resolve<AttributeValueRespository>();
            var reuslt = repository.DeleteAttributeValue(valueId);
            return reuslt;
        }

        public bool SetAttributeValue(int attributeId, string Value)
        {
            var reuslt = _attributeValueRepository.UpdateNonDefaults(new AttributeValueEntity()
            {
                ValueStr = Value,
                AttributeId = attributeId
            }, x => x.AttributeId == attributeId);
            return reuslt > 0;
        }
        public List<AttributeValueDto> GetAttributeValueList(int attributeId)
        {
            var retList = new List<AttributeValueDto>();
            var repository = IocManager.Instance.Resolve<AttributeValueRespository>();
            var list = repository.GetAttributeValueList(attributeId);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }

        private static AttributeValueDto ConvertFromRepositoryEntity(AttributeValueEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var dto = new AttributeValueDto
            {
                ValueId = entity.Id,
                AttributeId = entity.AttributeId.Value,
                DisplaySequence = entity.DisplaySequence.Value,
                ValueStr = entity.ValueStr
            };

            return dto;
        }
    }
}
