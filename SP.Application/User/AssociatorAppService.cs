using Lib.Application.Services;
using LibMain.Dependency;
using LibMain.Domain.Repositories;
using SP.Application.User.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User
{
    public class AssociatorAppService : ApplicationService, IAssociatorAppService
    {
        private readonly IRepository<AssociatorEntity, int> _userRepository;
        public AssociatorAppService(IRepository<AssociatorEntity, int> userRepository)
        {
            _userRepository = userRepository;
        }
        public bool AddAssociator(AssociatorDto associator)
        {
            var repository = IocManager.Instance.Resolve<SysKindRespository>();
            var kind = repository.GetAssociatorDetail(associator.KindId);
            var startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(7);
            if (kind!= null)
            {
                switch (kind.Unit)
                {
                    case 1:
                        endDate = startDate.AddDays(associator.Quantity);
                        break;
                    case 2:
                        endDate = startDate.AddMonths(associator.Quantity);
                        break;
                    case 3:
                        endDate = startDate.AddYears(associator.Quantity);
                        break;
                }
            }            
            var result = _userRepository.Insert(new AssociatorEntity()
            {
                 AccountId = associator.AccountId,
                 AssociatorId = Guid.NewGuid().ToString(),
                 KindId = associator.KindId,
                 Quantity = associator.Quantity,
                 Amount = associator.Amount,
                 StartDate = startDate,
                 EndDate = endDate,
                 PayType = 0,
                 Status = 1
            });
            return result != null;
        }
        public bool DeleteAssociator(string associatorId)
        {
            var result = false;
            try
            {
                var repository = IocManager.Instance.Resolve<AssociatorRespository>();
                repository.DeleteAssociator(associatorId);
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public List<AssociatorDto> GetAssociatorList(int pageIndex, int pageSize)
        {
            var retList = new List<AssociatorDto>();
            var repository = IocManager.Instance.Resolve<AssociatorRespository>();
            var list = repository.GetAssociatorList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }
        public int GetAssociatorListCount()
        {
            var retList = new List<AssociatorDto>();
            var repository = IocManager.Instance.Resolve<AssociatorRespository>();
            var ret = repository.GetAssociatorListCount();
            
            return ret;
        }
        public List<AssociatorDto> SearchAssociatorByAccountId(string accountId, int pageIndex, int pageSize)
        {
            var retList = new List<AssociatorDto>();
            var repository = IocManager.Instance.Resolve<AssociatorRespository>();
            var list = repository.SearchAssociatorByAccountId(accountId, pageIndex, pageSize);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }

        private static AssociatorDto ConvertFromRepositoryEntity(AssociatorEntity adminUser)
        {
            if (adminUser == null)
            {
                return null;
            }
            var adminDto = new AssociatorDto
            {
                AccountId = adminUser.AccountId,
                AssociatorId = adminUser.AssociatorId,
                Amount = adminUser.Amount != null ? adminUser.Amount.Value:0,
                Status = adminUser.Status.Value,
                KindId = adminUser.KindId,
                PayType = adminUser.PayType != null ? adminUser.PayType.Value:0,
                EndDate = adminUser.EndDate != null ?adminUser.EndDate.Value:DateTime.MinValue,
                StartDate = adminUser.StartDate != null ? adminUser.StartDate.Value : DateTime.MinValue,
                Quantity = adminUser.Quantity.Value,
            };
            if (!string.IsNullOrEmpty(adminDto.AccountId))
            {
                var accountRep = IocManager.Instance.Resolve<AccountRespository>();
                adminDto.FullName = accountRep.GetAccountInfoById(adminDto.AccountId)?.Fullname;
            }
            if (!string.IsNullOrEmpty(adminDto.KindId))
            {
                var kindRep = IocManager.Instance.Resolve<SysKindRespository>();
                adminDto.KindName = kindRep.GetAssociatorDetail(adminDto.KindId)?.Description;
            }
            return adminDto;
        }
    }
}
