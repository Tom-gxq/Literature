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
        public ShopReportDatabase(ShopRespository repository)
        {
            _repository = repository;
        }

        public List<ShopDomain> GetAllShopList(int regionId, int pageIndex, int pageSize)
        {
            var shopDomainList = new List<ShopDomain>();
            var addressList = _repository.GetShopList(regionId,pageIndex,  pageSize);
            foreach (var item in addressList)
            {
                var order = ConvertShopEntityToDomain(item);
                shopDomainList.Add(order);
            }
            return shopDomainList;
        }
        public int GetAllShopCount(int regionId)
        {
            return _repository.GetAllShopCount(regionId);
        }

        public ShopDomain GetShopById(int shopId)
        {
            var shop = _repository.GetShopById(shopId);
            var domain = ConvertShopEntityToDomain(shop);
            return domain;
        }
        private ShopDomain ConvertShopEntityToDomain(ShopEntity entity)
        {
            var account = new ShopDomain();
            account.SetMemento(entity);
            return account;
        }
    }
}
