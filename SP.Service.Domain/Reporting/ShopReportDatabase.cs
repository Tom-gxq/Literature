using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class ShopReportDatabase : IReportDatabase
    {
        private readonly ShopRespository _repository;
        private readonly ShopOwnerRespository _shopOwnerRepository;
        public ShopReportDatabase(ShopRespository repository, ShopOwnerRespository shopOwnerRepository)
        {
            _repository = repository;
            _shopOwnerRepository = shopOwnerRepository;
        }

        public List<ShopDomain> GetAllShopList(int regionId, int shopType, int pageIndex, int pageSize)
        {
            var shopDomainList = new List<ShopDomain>();
            var addressList = _repository.GetShopList(regionId, shopType, pageIndex,  pageSize);
            foreach (var item in addressList)
            {
                var order = ConvertShopEntityToDomain(item);
                shopDomainList.Add(order);
            }
            return shopDomainList;
        }
        public int GetAllShopCount(int regionId, int shopType)
        {
            return _repository.GetAllShopCount(regionId, shopType);
        }

        public ShopDomain GetShopById(int shopId)
        {
            var shop = _repository.GetShopById(shopId);
            var domain = ConvertShopEntityToDomain(shop);
            return domain;
        }

        public List<ShopOwnerDomain> GetAllShopOwnerList(int shopId)
        {
            var shopDomainList = new List<ShopOwnerDomain>();
            var addressList = _shopOwnerRepository.GetAllShopOwnerList(shopId);
            foreach (var item in addressList)
            {
                var order = ConvertShopOwnerEntityToDomain(item);
                shopDomainList.Add(order);
            }
            return shopDomainList;
        }

        public bool UpdateOpenShopStatus(string accountId, bool status)
        {
            var result = _shopOwnerRepository.UpdateOpenShopStatus(accountId, status);
            return result > 0;
        }

        public ShopOwnerDomain GetShopStatus(string accountId)
        {            
            var shopOwner = _shopOwnerRepository.GetShopStatus(accountId);
            var entity = ConvertShopOwnerEntityToDomain(shopOwner);
            return entity;
        }

        private ShopDomain ConvertShopEntityToDomain(ShopEntity entity)
        {
            var account = new ShopDomain();
            account.SetMemento(entity);
            return account;
        }

        private ShopOwnerDomain ConvertShopOwnerEntityToDomain(ShopOwnerEntity entity)
        {
            var account = new ShopOwnerDomain();
            account.SetMemento(entity);
            return account;
        }
    }
}
