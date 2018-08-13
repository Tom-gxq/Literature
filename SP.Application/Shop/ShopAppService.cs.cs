using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP.Application.Shop.DTO;
using Lib.Application.Services;
using LibMain.Domain.Repositories;
using SP.DataEntity;
using LibMain.Dependency;
using SP.ManageEntityFramework.Repositories;
using SP.Application.User.DTO;

namespace SP.Application.Shop
{
    public class ShopAppService : ApplicationService,IShopAppService
    {
        private readonly IRepository<ShopEntity, int> _shopRepository;
        private readonly IRepository<AccountInfoEntity, int> _accountInfoRepository;
        private readonly IRepository<ShopAttributeEntity, int> _shopAttributeRepository;
        private readonly IRepository<RegionEntity, int> _regionRepository;
        private readonly IRepository<ShopProductEntity, int> _shopProductRepository;
        public ShopAppService(IRepository<ShopEntity, int> shopRepository, IRepository<AccountInfoEntity, int> accountInfoRepository,
            IRepository<ShopAttributeEntity, int> shopAttributeRepository, IRepository<RegionEntity, int> regionRepository,
            IRepository<ShopProductEntity, int> shopProductRepository)
        {
            _shopRepository = shopRepository;
            _accountInfoRepository = accountInfoRepository;
            _shopAttributeRepository = shopAttributeRepository;
            _regionRepository = regionRepository;
            _shopProductRepository = shopProductRepository;
        }
        public bool AddShop(string shopName, int displaySequence, string startTime, string endTime, int shopType, string shopLogo)
        {
            var result = _shopRepository.Insert(new ShopEntity()
            {
                ShopName = shopName,
                DisplaySequence = displaySequence,
                MetaKeywords = shopName,
                StartTime = startTime,
                EndTime = endTime,
                ShopType = shopType,
                ShopLogo = shopLogo
            });
            return result != null;
        }

        public bool DelShop(int id)
        {
            var repository = IocManager.Instance.Resolve<ShopRespository>();
            return repository.DeleteShop(id);
        }

        public bool EditShop(ShopDto shop)
        {
            if(!string.IsNullOrEmpty(shop.OwnerId))
            {
                var account = _accountInfoRepository.Single(x => x.AccountId == shop.OwnerId);
                shop.Owner = ConvertFromRepositoryEntity(account);                
            }
            var result = _shopRepository.UpdateNonDefaults(new ShopEntity()
            {
                Id = shop.Id,
                DisplaySequence = shop.DisplaySequence,
                OwnerId = (!string.IsNullOrEmpty(shop.OwnerId) ? shop.OwnerId : null),
                ShopName = shop.ShopName,
                RegionId = shop.RegionId,
                MetaKeywords = shop.ShopName+"|"+ (shop.Owner != null ? shop.Owner.Fullname:string.Empty),
                StartTime = shop.StartTime,
                EndTime = shop.EndTime,
                ShopType = shop.ShopType,
                ShopLogo = shop.ShopLogo
            }, x => x.Id == shop.Id);

            if(result > 0 && shop.AttributeId > 0)
            {
                var attribute = _shopAttributeRepository.Single(x=>x.ShopId == shop.Id);
                if(attribute != null)
                {
                    _shopAttributeRepository.UpdateNonDefaults(new ShopAttributeEntity()
                    {
                       ShopId = shop.Id,
                       AttributeId = shop.AttributeId
                    },x=>x.ShopId == shop.Id);
                }
                else
                {
                    _shopAttributeRepository.Insert(new ShopAttributeEntity()
                    {
                       ShopId = shop.Id,
                       AttributeId = shop.AttributeId
                    });
                }
            }
            return result > 0;
        }

        public ShopDto GetShopDetail(int id)
        {
            var shop = _shopRepository.Single(x => x.Id == id);
            var shopDto = ConvertFromRepositoryEntity(shop);
            if (shop != null && !string.IsNullOrEmpty(shop.OwnerId))
            {
                var account = _accountInfoRepository.Single(x=> x.AccountId == shop.OwnerId);
                shopDto.Owner = ConvertFromRepositoryEntity(account);
            }
            if (shop != null )
            {
                var attribte = _shopAttributeRepository.Single(x => x.ShopId == shop.Id);
                shopDto.AttributeId = attribte?.AttributeId??0;
            }
            if(shop.RegionId != null && shop.RegionId.Value > 0)
            {
                var region = _regionRepository.Single(x=>x.Id == shop.RegionId);
                shopDto.SchoolId = region.ParentDataID != null ? region.ParentDataID.Value : 0;
                if(shopDto.SchoolId > 0)
                {
                    region = _regionRepository.Single(x => x.Id == shopDto.SchoolId);
                    shopDto.CityId = region.ParentDataID != null ? region.ParentDataID.Value : 0;                    
                }
            }
            return shopDto;
        }

        public List<ShopDto> GetShopList(int pageIndex, int pageSize)
        {
            var retList = new List<ShopDto>();
            var repository = IocManager.Instance.Resolve<ShopRespository>();
            var list = repository.GetShopList(pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                if (item != null && !string.IsNullOrEmpty(item.OwnerId))
                {
                    var account = _accountInfoRepository.Single(x => x.AccountId == item.OwnerId);
                    entity.Owner = ConvertFromRepositoryEntity(account);
                }
                if (item != null && item.RegionId >0)
                {
                    var region = _regionRepository.Single(x => x.Id == item.RegionId);
                    entity.RegionName = region?.DataName??string.Empty;
                }
                retList.Add(entity);
            }

            return retList;
        }

        public int GetShopListCount()
        {
            var total = _shopRepository.Count();
            return total;
        }

        public List<ShopDto> GetShopListByRegionId(int regionId)
        {
            var retList = new List<ShopDto>();
            var repository = IocManager.Instance.Resolve<ShopRespository>();
            var list = repository.GetShopListByRegionId(regionId);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                
                retList.Add(entity);
            }

            return retList;
        }
        public List<ShopDto> GetFoodShopListByRegionId(int regionId, int marketId)
        {
            var retList = new List<ShopDto>();
            var repository = IocManager.Instance.Resolve<ShopRespository>();
            var list = repository.GetFoodShopListByRegionId(regionId, marketId);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);

                retList.Add(entity);
            }

            return retList;
        }
        public List<ShopDto> GetMarketShopListByRegionId(int regionId, int marketId)
        {
            var retList = new List<ShopDto>();
            var repository = IocManager.Instance.Resolve<ShopRespository>();
            var list = repository.GetMarketShopListByRegionId(regionId, marketId);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);

                retList.Add(entity);
            }

            return retList;
        }

        public List<ShopDto> SearchShopByUserName(string keyWord, int pageIndex, int pageSize)
        {
            var retList = new List<ShopDto>();
            var repository = IocManager.Instance.Resolve<ShopRespository>();
            var list = repository.SearchShopByName(keyWord, pageIndex, pageSize);
            foreach (var item in list)
            {
                var entity = ConvertFromRepositoryEntity(item);
                if (item != null && !string.IsNullOrEmpty(item.OwnerId))
                {
                    var account = _accountInfoRepository.Single(x => x.AccountId == item.OwnerId);
                    entity.Owner = ConvertFromRepositoryEntity(account);
                }
                if (item != null && item.RegionId > 0)
                {
                    var region = _regionRepository.Single(x => x.Id == item.RegionId);
                    entity.RegionName = region.DataName;
                }
                retList.Add(entity);
            }
            return retList;
        }

        public int SearchShopByUserNameCount(string keyWord)
        {
            var repository = IocManager.Instance.Resolve<ShopRespository>();
            var total = repository.SearchShopByNameCount(keyWord);

            return total;
        }
        public bool AddShopProduct(int ShopId, string ProductId)
        {
            var result = _shopProductRepository.Insert(new ShopProductEntity()
            {
                ProductId = ProductId,
                ShopId = ShopId
            });
            return (result != null);
        }
        public bool DelShopProductByShopId(int ShopId)
        {
            var repository = IocManager.Instance.Resolve<ShopProductRespository>();
            return repository.DelShopProductByShopId(ShopId);
        }

        public bool AddShopOwner(ShopOwnerDto dto)
        {
            var repository = IocManager.Instance.Resolve<ShopOwnerRespository>();
            return repository.AddShopOwner(new ShopOwnerEntity()
            {
                OwnerId = dto.OwnerId,
                ShopId = dto.ShopId,
                ShopStatus = false,
            });
        }
        public bool DelShopOwner(int shopId,string accountId)
        {
            var repository = IocManager.Instance.Resolve<ShopOwnerRespository>();
            return repository.DelShopOwner(shopId, accountId);
        }
        public ShopOwnerDto GetShopOwnerByAccountId(string accountId)
        {
            var repository = IocManager.Instance.Resolve<ShopOwnerRespository>();
            var entity = repository.GetShopOwnerByAccountId(accountId);
            return ConvertFromRepositoryEntity(entity);
        }

        private static ShopDto ConvertFromRepositoryEntity(ShopEntity shop)
        {
            if (shop == null)
            {
                return null;
            }
            var shopDto = new ShopDto
            {
                Id = shop.Id,
                DisplaySequence = shop.DisplaySequence.Value,
                ShopName = shop.ShopName,
                OwnerId = shop.OwnerId,
                RegionId = shop.RegionId != null ? shop.RegionId.Value:0,
                StartTime = shop.StartTime,
                EndTime = shop.EndTime,
                ShopType = shop.ShopType != null ? shop.ShopType.Value:0,
                ShopLogo = shop.ShopLogo != null ? shop.ShopLogo : string.Empty,
            };

            return shopDto;
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
                 Fullname = accountInfo.Fullname
            };

            return accountInfoDto;
        }

        private static ShopOwnerDto ConvertFromRepositoryEntity(ShopOwnerEntity shop)
        {
            if (shop == null)
            {
                return null;
            }
            var shopDto = new ShopOwnerDto
            {
                OwnerId = shop.OwnerId,
                ShopId = shop.ShopId.Value,                
            };

            return shopDto;
        }
    }
}
