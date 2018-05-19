using Lib.Application.Services;
using LibMain.Dependency;
using LibMain.Domain.Repositories;
using SP.Application.User.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.User
{
    public class AdminAppService: ApplicationService, IAdminAppService
    {
        private readonly IRepository<AdminEntity, long> _userRepository;
        private readonly IRepository<CashApplyEntity, int> _cashApplyRepository;
        public AdminAppService(IRepository<AdminEntity, long> userRepository, IRepository<CashApplyEntity, int> cashApplyRepository)
        {
            _userRepository = userRepository;
            _cashApplyRepository = cashApplyRepository;
        }

        public AdminDto GetCurrentSession()
        {
            var current= this.AbpSession["current"];
            if(current != null)
            {
                return current as AdminDto;
            }
            else
            {
                return null;
            }
        }

        public bool DelCurrentSession()
        {
            this.AbpSession["current"] = null;
            return true;
        }
        public AdminDto GetUsers(string userName)
        {
            var adminUser = _userRepository.Single(a => (a.UserName == userName));
            if (adminUser == null)
            {
                return null;
            }
            else
            {
                return ConvertFromRepositoryEntity(adminUser);
            }
        }

        public bool CheckAdminLogin(string userName, string passWord)
        {
            bool ret = false;
            var adminDto = GetUsers(userName);
            if(adminDto!= null)
            {
                if(adminDto.Password == GetMD5(passWord))
                {
                    this.AbpSession["current"] = adminDto;
                    ret = true;
                }
            }
            return ret;
        }

        public bool AddAdmin(string userName, string passWord)
        {
            var result = _userRepository.Insert(new AdminEntity()
            {
                UserName = userName,
                PassWord = GetMD5(passWord),
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            });
            return result != null;
        }
        public bool DelAdmin(int id)
        {
            var result = false;
            try
            {
                _userRepository.UpdateNonDefaults(new AdminEntity
                {
                    Id =id,
                    IsDel =true
                },x=>x.Id == id);
                result = true;
            }
            catch(Exception ex)
            {

            }
            return result;
        }
        public List<AdminDto> GetAdminList(int pageIndex, int pageSize)
        {
            var retList = new List<AdminDto>();
            var repository = IocManager.Instance.Resolve<AdminRespository>();
            var list = repository.GetAdminList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }

        public int GetAdminListCount()
        {
            var repository = IocManager.Instance.Resolve<AdminRespository>();
            var total = repository.GetAdminListCount();
            
            return total;
        }

        public int SearchAdminByUserNameCount(string userName)
        {
            var repository = IocManager.Instance.Resolve<AdminRespository>();
            var total = repository.SearchAdminByUserNameCount(userName);

            return total;
        }

        public List<AdminDto> SearchAdminByUserName(string userName)
        {
            var retList = new List<AdminDto>();
            var repository = IocManager.Instance.Resolve<AdminRespository>();
            var list = repository.SearchAdminByUserName(userName);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }

        public AdminDto GetAdminDetail(int id)
        {
            var entity = _userRepository.Single(x => x.Id == id);
            return ConvertFromRepositoryEntity(entity);
        }

        public bool EditAdmin(AdminDto admin)
        {
            var result = _userRepository.UpdateNonDefaults(new AdminEntity()
            {
                PassWord = GetMD5(admin.Password),
                UserName = admin.UserName,
                UpdateTime = DateTime.Now,
                Id = admin.Id
            }, x => x.Id == admin.Id);
            return result > 0;
        }
        public List<CashApplyDto> GetCashApply(int pageIndex, int pageSize)
        {
            var retList = new List<CashApplyDto>();
            var repository = IocManager.Instance.Resolve<CashApplyRespository>();
            var list = repository.GetCashApplyList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var adminUser = ConvertFromRepositoryEntity(item);
                retList.Add(adminUser);
            }
            return retList;
        }
        public long GetCashApplyCount()
        {
            var repository = IocManager.Instance.Resolve<CashApplyRespository>();
            return repository.GetCashApplyListCount();
        }

        public List<CashApplyDto> SearchCashApplyByKeyWord(string keyWord)
        {
            var retList = new List<CashApplyDto>();
            var repository = IocManager.Instance.Resolve<AccountRespository>();
            var accountList = repository.SearchAccount(keyWord);
            if(accountList != null && accountList.Count > 0)
            {
                var cashRep = IocManager.Instance.Resolve<CashApplyRespository>();
                var list = cashRep.GetCashApplyList(accountList[0].AccountId);
                foreach (var item in list)
                {
                    var adminUser = ConvertFromRepositoryEntity(item);
                    retList.Add(adminUser);
                }
            }
            return retList;
        }
        public List<CashApplyDto> SearchCashApplyByDate(int status,string startDate,string endDate)
        {
            var retList = new List<CashApplyDto>();
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate))
            {
                var cashRep = IocManager.Instance.Resolve<CashApplyRespository>();
                var start = DateTime.Parse(startDate);
                var end = DateTime.Parse(endDate);
                var list = cashRep.SearchCashApplyByDate(status, start, end);
                foreach (var item in list)
                {
                    var adminUser = ConvertFromRepositoryEntity(item);
                    retList.Add(adminUser);
                }
            }
            return retList;
        }
        public bool EditApplyStatus(int id, int status)
        {
            bool retValue = false;
            try
            {
                var repository = IocManager.Instance.Resolve<CashApplyRespository>();
                var result = repository.UpdateNonDefaults(new CashApplyEntity()
                {
                    Status = status,
                    UpdateTime = DateTime.Now,
                }, x => x.Id == id);
                if (result > 0)
                {
                    var cash = repository.GetCashApplyById(id);
                    if (cash != null)
                    {
                        var finaceRep = IocManager.Instance.Resolve<AccountFinanceRepository>();
                        var finace = finaceRep.GetAccountFinanceDetail(cash.AccountId);
                        if (finace != null)
                        {
                            finaceRep.UpdateNonDefaults(new AccountFinanceEntity()
                            {
                                AccountId = cash.AccountId,
                                UseAmount = (finace.UseAmount != null? finace.UseAmount.Value :0 )+ cash.Money,                                
                            }, x => x.Id == finace.Id);
                        }
                    }
                    retValue = true;
                }
            }
            catch(Exception ex)
            {
                retValue = false;
            }
            return retValue;
        }

        private static AdminDto ConvertFromRepositoryEntity(AdminEntity adminUser)
        {
            if (adminUser == null)
            {
                return null;
            }
            var adminDto = new AdminDto
            {
                Id = adminUser.Id,
                Password = adminUser.PassWord,
                UserName = adminUser.UserName,
                CreateTime = adminUser.CreateTime.Value,
                UpdateTime = adminUser.UpdateTime.Value,
                Status = adminUser.IsDel
            };

            return adminDto;
        }
        private static CashApplyDto ConvertFromRepositoryEntity(CashApplyEntity cashApply)
        {
            if (cashApply == null)
            {
                return null;
            }
            var cashApplyDto = new CashApplyDto
            {
                Id = cashApply.Id != null ? cashApply.Id.Value:0,
                Alipay = cashApply.Alipay,
                Money = cashApply.Money,
                CreateTime = cashApply.CreateTime != null ? cashApply.CreateTime.Value:DateTime.MinValue,
                Status = cashApply.Status.Value
            };
            if(!string.IsNullOrEmpty(cashApply.AccountId))
            {
                var repository = IocManager.Instance.Resolve<AccountInfoRepository>();
                var entity = repository.GetAccountInfoById(cashApply.AccountId);
                if (entity != null)
                {
                    cashApplyDto.AccountInfo = ConvertFromRepositoryEntity(entity);
                }
                var finaceRep = IocManager.Instance.Resolve<AccountFinanceRepository>();
                var finaceEntity = finaceRep.GetAccountFinanceDetail(cashApply.AccountId);
                cashApplyDto.HaveAmount = finaceEntity.HaveAmount != null ? finaceEntity.HaveAmount.Value:0;
            }

            return cashApplyDto;
        }

        private static string GetMD5(string myString)
        {
            byte[] result = Encoding.Default.GetBytes(myString);    //tbPass为输入密码的文本框
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            var retVal = BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本
            return retVal;
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
    }
}
