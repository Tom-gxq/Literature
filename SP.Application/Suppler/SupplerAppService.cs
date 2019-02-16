using Lib.Application.Services;
using LibMain.Dependency;
using LibMain.Domain.Repositories;
using SP.Application.Suppler.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Suppler
{
    public class SupplerAppService : ApplicationService, ISupplerAppService
    {
        private readonly IRepository<SuppliersEntity, int> _shopRepository;
        public SupplerAppService(IRepository<SuppliersEntity, int> shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public bool AddSuppler(SupplerDto dto)
        {
            var result = _shopRepository.Insert(new SuppliersEntity()
            {
                SuppliersName = dto.SuppliersName,
                Status = 0,
                AccountId = dto.AccountId,
                AlipayNo = dto.AlipayNo,
                AuthorizationPath = dto.AuthorizationPath,
                LicensePath = dto.LicensePath,
                LogoPath = dto.LogoPath,
                PermitPath = dto.PermitPath,
                TelPhone = dto.TelPhone,
                CreateTime = DateTime.Now
            });
            return result != null;
        }

        public bool DelSuppler(int id)
        {
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            return repository.UpdateSupplerStatus(new SuppliersEntity()
            {
                 Id = id,
                 Status = 1,
                 UpdateTime = DateTime.Now
            });
        }

        public bool DelSupplerById(int id)
        {
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            return repository.DeleteById(id) > 0;
        }

        public bool UpdateSeller(SupplerDto dto)
        {
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            return repository.UpdateSuppler(new SuppliersEntity()
            {
                Id = dto.Id,
                AlipayNo = string.IsNullOrEmpty(dto.AlipayNo)?null: dto.AlipayNo,
                AuthorizationPath = string.IsNullOrEmpty(dto.AuthorizationPath) ? null : dto.AuthorizationPath,
                LicensePath = string.IsNullOrEmpty(dto.LicensePath) ? null : dto.LicensePath,
                LogoPath = string.IsNullOrEmpty(dto.LogoPath) ? null : dto.LogoPath,
                PermitPath = string.IsNullOrEmpty(dto.PermitPath) ? null : dto.PermitPath,
                SuppliersName= string.IsNullOrEmpty(dto.SuppliersName) ? null : dto.SuppliersName,
                TelPhone = string.IsNullOrEmpty(dto.TelPhone) ? null : dto.TelPhone,
                UpdateTime = DateTime.Now
            });
        }
        public List<SupplerDto> GetSupplerList()
        {
            var retList = new List<SupplerDto>();
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            var list = repository.GeAllSupplerList();
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);

                retList.Add(entity);
            }

            return retList;
        }

        public List<SupplerDto> GetSupplerList(int pageIndex, int pageSize)
        {
            var retList = new List<SupplerDto>();
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            var list = repository.GetSellerList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);

                retList.Add(entity);
            }

            return retList;
        }

        public int GetSellerCount()
        {
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            return repository.GetSellerCount();
        }

        public List<SupplerDto> SearchSuppler(string productId, int supplerId, int type, int pageIndex, int pageSize)
        {
            var retList = new List<SupplerDto>();
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            var list = repository.SearchSuppler(productId, supplerId, type, pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);

                retList.Add(entity);
            }

            return retList;
        }
        public int SearchSupplerCount(string productId, int supplerId, int type)
        {
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            var count = repository.SearchSupplerCount(productId, supplerId, type);
            return count;
        }
        public SupplerDto GetSupplerDetail(int id)
        {
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            var entity =  repository.GetSupplerById(id);
            return ConvertFromRepositoryEntity(entity);
        }
        public List<SupplerDto> SearchSellerData(string name)
        {
            var retList = new List<SupplerDto>();
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            var list = repository.SearchSellerData(name);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);

                retList.Add(entity);
            }

            return retList;
        }

        public SupplerDto GetSellerDataByAccountId(string accountId)
        {
            var retList = new List<SupplerDto>();
            var repository = IocManager.Instance.Resolve<SupplersRepository>();
            var entity = repository.GetSellerDataByAccountId(accountId);
            
            return ConvertFromRepositoryEntity(entity);
        }
        private static SupplerDto ConvertFromRepositoryEntity(SuppliersEntity entity)
        {
            if (entity == null)
            {
                return null;
            }
            var shopDto = new SupplerDto
            {
                Id = entity.Id,
                AccountId = entity.AccountId,
                AlipayNo = entity.AlipayNo,
                AuthorizationPath = entity.AuthorizationPath,
                LicensePath = entity.LicensePath,
                LogoPath = entity.LogoPath,
                PermitPath = entity.PermitPath,
                SuppliersName = entity.SuppliersName,
                TelPhone = entity.TelPhone,
                CreateTime = entity.CreateTime != null ? entity.CreateTime.Value : DateTime.MinValue,
                UpdateTime = entity.UpdateTime != null ? entity.UpdateTime.Value : DateTime.MinValue,
                Status = (int)entity.Status
            };

            return shopDto;
        }
    }
}
