using Lib.Application.Services;
using SP.Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product
{
    public interface IAttributeValueAppService : IApplicationService
    {
        bool AddAttributeValue(AttributeValueDto attributeValue);
        bool UpdateAttributeValueDisplaySequence(int attributeId, int displaySequence);
        bool SetAttributeValue(int attributeId, string Value);
        bool DeleteAttributeValue(int attributeId);
        List<AttributeValueDto> GetAttributeValueList(int attributeId);
    }
}
