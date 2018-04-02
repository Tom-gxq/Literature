using Lib.Application.Services;
using SP.Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product
{
    public interface IBrandAppService : IApplicationService
    {
        bool AddBrand(BrandDto brand);
        bool UpdateBrandDisplaySequence(int brandId, int displaySequence);
        bool DeleteBrand(int brandId);
        int GetBrandListCount();
        List<BrandDto> GetBrandList(int pageIndex, int pageSize);
        int SearchBrandByNameCount(string brandName);
        List<BrandDto> SearchBrandByName(string brandName, int pageIndex, int pageSize);
        bool EditBrand(BrandDto productType);
        BrandDto GetBrandDetail(int id);
        List<BrandDto> GetAllBrandList();
    }
}
