using Lib.Application.Services;
using SP.Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product
{
    public interface IProductTypeService: IApplicationService
    {
        bool AddProductType(string typeName, int displaySequence);
        bool DelProductType(int id);
        List<ProductTypeDto> GetProductTypeList(int pageIndex, int pageSize);
        List<ProductTypeDto> SearchProductTypeByName(string typeName, int pageIndex, int pageSize);
        bool EditProductType(ProductTypeDto productType);
        bool DeleteProductTypeById(int id);
        ProductTypeDto GetProductTypeDetail(int id);
        int GetProductTypeListCount();
        int SearchProductTypeByNameCount(string typeName);
        List<ProductTypeDto> GetAllProductTypeList();
    }
}
