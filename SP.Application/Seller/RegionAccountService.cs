using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibMain.Dependency;
using SP.Application.Seller.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;

namespace SP.Application.Seller
{
    public class RegionAccountService : IRegionAccountService
    {
        public bool AddLeader(RegionAccountDto data)
        {
            var repository = IocManager.Instance.Resolve<RegionAccountRespository>();
            return repository.Add(new RegionAccountEntity()
            {
                Id = data.AccountId,
                RegionId = data.RegionId,
                Status = data.Status
            });
        }

        public bool DelLeader(string accountId, int regionId)
        {
            var repository = IocManager.Instance.Resolve<RegionAccountRespository>();
            return repository.UpdateLeader(new RegionAccountEntity()
            {
                Id = accountId,
                RegionId = regionId,
                Status = 1
            });
        }

        public long GetLeaderCount()
        {
            var repository = IocManager.Instance.Resolve<RegionAccountRespository>();
            var count = repository.Count();
            return count;
        }

        public RegionAccountDto GetLeaderDetail(int id)
        {
            throw new NotImplementedException();
        }

        public List<RegionAccountDto> GetLeaderList(int pageIndex, int pageSize)
        {
            var retList = new List<RegionAccountDto>();
            var repository = IocManager.Instance.Resolve<RegionAccountRespository>();
            var list = repository.GetRegionAccount(pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }

        private static RegionAccountDto ConvertFromRepositoryEntity(RegionAccountEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            
            var accountRepos = IocManager.Instance.Resolve<AccountRespository>();
            var accountInfo = accountRepos.GetAccountInfoById(entity.Id);
            var regionRepos = IocManager.Instance.Resolve<RegionRespository>();
            var regionInfo = regionRepos.GetRegionDataDetail((int)entity.RegionId);

            var regionAccountDto = new RegionAccountDto
            {
                AccountId = entity.Id,
                AccountName= accountInfo?.Fullname,
                RegionId = (int)entity?.RegionId,
                RegionName = regionInfo?.DataName
            };

            return regionAccountDto;
        }

        public List<RegionAccountDto> SearchLeaderByName(string leaderName)
        {
            throw new NotImplementedException();
        }

        public int SearchLeaderByNameCount(string leaderName)
        {
            throw new NotImplementedException();
        }

        public bool UpdateLeader(RegionAccountDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
