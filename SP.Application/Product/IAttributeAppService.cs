using Lib.Application.Services;
using SP.Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product
{
    public interface IAttributeAppService : IApplicationService
    {
        bool AddAttribute(AttributeDto attribute);
        bool UpdaeAttributeDisplaySequence(int attributeId, int displaySequence);
        bool DeleteAttribute(int attributeId);
        int GetAttributeListCount();
        List<AttributeDto> GetAttributeList(int pageIndex, int pageSize);
        int SearchAttributeByNameCount(string brandName);
        List<AttributeDto> SearchAttributeByName(string brandName, int pageIndex, int pageSize);
        bool EditAttribute(AttributeDto attribute);
        AttributeDto GetAttributeDetail(int id);
    }
}
