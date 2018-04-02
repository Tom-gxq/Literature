using Lib.Application.Services;
using SP.Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product
{
    public interface IRegionAppService : IApplicationService
    {
        List<RegionDto> GetRegionData(int dataType);
        List<RegionDto> GetChildRegionData(int parentId, int pageIndex, int pageSize);
        List<RegionDto> GetChildRegionData(int parentId);
        bool AddRegionData(RegionDto region);
        bool DelRegionData(int dataId);
        long GetRegionDataCount(int dataType);
        long GetChildRegionDataCount(int parentId);
        List<RegionDto>  SearchRegionData(string dataName);
        RegionDto GetRegionDataDetail(int dataId);
        bool EditRegionData(RegionDto region);
    }
}
