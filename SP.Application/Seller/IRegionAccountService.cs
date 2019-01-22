using Lib.Application.Services;
using SP.Application.Seller.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Seller
{
    public interface IRegionAccountService : IApplicationService
    {
        bool AddLeader(RegionAccountDto data);
        bool DelLeader(string accountId, int regionId);
        bool UpdateLeader(RegionAccountDto dto);
        List<RegionAccountDto> GetLeaderList(int pageIndex, int pageSize);
        long GetLeaderCount();
        RegionAccountDto GetLeaderDetail(int id);
        List<RegionAccountDto> SearchLeaderByName(string leaderName);
        int SearchLeaderByNameCount(string leaderName);
    }
}
