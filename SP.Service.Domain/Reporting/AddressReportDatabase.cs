using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AddressReportDatabase : IReportDatabase
    {
        private readonly AccountAddressRepository _repository;
        private readonly RegionDataRepository _regionDataRepository;
        public AddressReportDatabase(AccountAddressRepository repository, RegionDataRepository regionDataRepository)
        {
            _repository = repository;
            _regionDataRepository = regionDataRepository;
        }

        public void Add(AccountAddressEntity item)
        {
            _repository.AddAccountAddress(item);
        }

        public List<AccountAddressDomain> GetAddressList(string accountId)
        {
            var addressDomainList = new List<AccountAddressDomain>();
            var addressList = _repository.GetAddressList(accountId);
            foreach (var item in addressList)
            {
                var order = ConvertAddressEntityToDomain(item);
                addressDomainList.Add(order);
            }
            return addressDomainList;
        }
        public AccountAddressDomain GetDefaultSelectedAddress(string accountId)
        {
            var addressDomain = new AccountAddressDomain();
            var addressList = _repository.GetAddressList(accountId);
            if(addressList != null && addressList.Count > 0)
            {
                addressDomain = ConvertAddressEntityToDomain(addressList[0]);
            }
            return addressDomain;
        }

        public AccountAddressDomain GetAddressById(int addressId,string accountId)
        {
            var address = _repository.GetAddressById(addressId,accountId);
            return ConvertAddressEntityToDomain(address); ;
        }
        public AccountAddressDomain GetAccountAddress(int dormId, string accountId)
        {
            var address = _repository.GetAddressById(dormId, accountId);
            return ConvertAddressEntityToDomain(address); ;
        }

        public bool RemoveAccountAddress(int addressId)
        {
            return _repository.RemoveAccountAddress(addressId);
        }

        public bool UpdateAccountAddress(AccountAddressEntity entity)
        {
            var result = _repository.UpdateAccountAddress(entity);
            return result > 0;
        }
        public bool EnableAccountAddress(AccountAddressEntity entity)
        {
            var result = _repository.EnableAccountAddress(entity);
            return result > 0;
        }
        public List<RegionDataDomain> GetRegionDataList(int dataType)
        {
            var domainList = new List<RegionDataDomain>();
            var list =_regionDataRepository.GetRegionDataList(dataType);
            foreach (var item in list)
            {
                var region = ConvertRegionDataEntityToDomain(item);
                domainList.Add(region);
            }
            return domainList;
        }

        public RegionDomain GetRegionData(int dataId)
        {
            var account = _regionDataRepository.GetRegionData(dataId);

            return ConvertRegionEntityToDomain(account); ;
        }
        public List<RegionDataDomain> GetChildRegionData(int dataId)
        {
            var domainList = new List<RegionDataDomain>();
            var list = _regionDataRepository.GetChildRegionData(dataId);
            foreach (var item in list)
            {
                var region = ConvertRegionDataEntityToDomain(item);
                domainList.Add(region);
            }
            return domainList;
        }
        public List<RegionDataDomain> GetChildRegionData(int dataId,DateTime updateTime)
        {
            var domainList = new List<RegionDataDomain>();
            var list = _regionDataRepository.GetChildRegionData(dataId, updateTime);
            foreach (var item in list)
            {
                var region = ConvertRegionDataEntityToDomain(item);
                domainList.Add(region);
            }
            return domainList;
        }

        public List<RegionDataDomain>  GetSelectedRegionDataList(string accountId)
        {
            var domainList = new List<RegionDataDomain>();
            var list = _regionDataRepository.GetSelectedRegionDataList(accountId);
            foreach (var item in list)
            {
                var region = ConvertRegionDataEntityToDomain(item);
                domainList.Add(region);
            }
            return domainList;
        }

        public List<RegionDataDomain> GetChildRegionDataList(int dataId, DateTime updateTime)
        {
            var domainList = new List<RegionDataDomain>();
            var list = _regionDataRepository.GetChildRegionData(dataId, updateTime);
            foreach (var item in list)
            {
                var region = ConvertRegionDataEntityToDomain(item);
                domainList.Add(region);
            }
            return domainList;
        }
        public List<RegionDataDomain> GetChildRegionDataList(int dataId)
        {
            var domainList = new List<RegionDataDomain>();
            var list = _regionDataRepository.GetChildRegionData(dataId);
            foreach (var item in list)
            {
                var region = ConvertRegionDataEntityToDomain(item);
                domainList.Add(region);
            }
            return domainList;
        }

        private AccountAddressDomain ConvertAddressEntityToDomain(AccountAddressEntity entity)
        {
            if(entity == null)
            {
                return null;
            }
            var account = new AccountAddressDomain();
            account.SetMemento(entity);
            var districtReg = _regionDataRepository.GetRegionData(account.DistrictId);
            if(districtReg != null)
            {
                account.BuildingId = districtReg.BuiddingID;
                account.BuildingName = districtReg.BuiddingName;
                account.DistrictName = districtReg.DistrictName;
                account.DistrictId = districtReg.DistrictID;
                account.SchoolId = districtReg.SchoolID;
                account.SchoolName = districtReg.SchoolName;
                account.DormId = districtReg.DataID;
                account.DormName = districtReg.DataName;
            }

            return account;
        }

        private RegionDataDomain ConvertRegionDataEntityToDomain(RegionDataEntity entity)
        {
            var region = new RegionDataDomain();
            region.SetMemento(entity);
            return region;
        }

        private RegionDomain ConvertRegionEntityToDomain(RegionEntity entity)
        {
            var region = new RegionDomain();
            region.SetMemento(entity);
            return region;
        }

    }
}
