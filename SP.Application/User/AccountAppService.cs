using Lib.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Application.User.DTO;
using LibMain.Dependency;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;

namespace SP.Application.User
{
    public class AccountAppService : ApplicationService, IAccountAppService
    {
        public List<AccountInfoDto> SearchAccount(string keywords)
        {
            var retList = new List<AccountInfoDto>();
            var repository = IocManager.Instance.Resolve<AccountRespository>();
            var list = repository.SearchAccount(keywords);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                retList.Add(entity);
            }

            return retList;
        }
        public AccountInfoDto GetAccountInfo(string accountId)
        {
            var retList = new List<AccountInfoDto>();
            var repository = IocManager.Instance.Resolve<AccountRespository>();
            var item = repository.GetAccountInfoById(accountId);
            var entity = ConvertFromRepositoryEntity(item);

            return entity;
        }

        public bool UpdateAccountUserType(string accountId,int userType)
        {
            var retList = new List<AccountInfoDto>();
            var repository = IocManager.Instance.Resolve<AccountInfoRepository>();
            var result = repository.UpdateAccountFullInfo(new AccountInfoEntity()
            {
                AccountId = accountId,
                UserType = userType
            });
            return result;
        }

        private static AccountInfoDto ConvertFromRepositoryEntity(AccountInfoEntity accountInfo)
        {
            if (accountInfo == null)
            {
                return null;
            }
            var accountInfoDto = new AccountInfoDto
            {
                AccountId = accountInfo.AccountId,
                Fullname = !string.IsNullOrEmpty(accountInfo.Fullname) ? accountInfo.Fullname : "@##$%",
            };

            return accountInfoDto;
        }

        public List<AccountInfoDto> GetAccountList()
        {
            var retList = new List<AccountInfoDto>();
            var repository = IocManager.Instance.Resolve<AccountInfoRepository>();

            foreach (var item in repository.GetAllList())
            {
                retList.Add(ConvertFromRepositoryEntity(item));
            }

            return retList;
        }
    }
}
