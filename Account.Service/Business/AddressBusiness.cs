using SP.Service;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Service.Business
{
    public class AddressBusiness
    {
        public static AccountResultResponse AddAccountAddress(AddressRequest request)
        {
            ServiceLocator.CommandBus.Send(new CreatAddressCommand(Guid.NewGuid(), request.Address.ContactName, request.Address.Gender
                , request.Address.ContactMobile, request.Address.DistrictId, request.Address.ContactAddress, request.Address.AccountId, request.Address.DormName, request.Address.IsDefault));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AccountResultResponse EditAccountAddress(AddressRequest request)
        {
            ServiceLocator.CommandBus.Send(new EditAddressCommand(Guid.NewGuid(), request.Address.Id, request.Address.ContactName, request.Address.Gender
                , request.Address.ContactMobile, request.Address.DistrictId, request.Address.ContactAddress, request.Address.AccountId, request.Address.DormName,request.Address.IsDefault));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AccountResultResponse UpdateAddressStatus(AddressStatusRequest request)
        {
            var ret = ServiceLocator.AddressDatabase.EnableAccountAddress(new SP.Service.Entity.AccountAddressEntity()
            {
                AccountId = request.AccountId,
                IsDefault = 0
            });

            var retValue = false;
            retValue = ServiceLocator.AddressDatabase.UpdateAccountAddress(new SP.Service.Entity.AccountAddressEntity()
            {
                ID = request.Id,
                AccountId = request.AccountId,
                IsDefault = request.Status
            });
            var result = new AccountResultResponse();
            if (retValue)
            {
                result.Status = 10001;
            }
            else
            {
                result.Status = 10002;
            }
            
            return result;
        }
        public static AccountResultResponse UpdateAddressDorm(AddressDormRequest request)
        {
            var retValue = false;
            retValue = ServiceLocator.AddressDatabase.UpdateAccountAddress(new SP.Service.Entity.AccountAddressEntity()
            {
                ID = request.Id,
                AccountId = request.AccountId,
                RegionID = request.DormId
            });
            var result = new AccountResultResponse();
            if (retValue)
            {
                result.Status = 10001;
            }
            else
            {
                result.Status = 10002;
            }

            return result;
        }

        public static AccountResultResponse DelAddress(DelAddressRequest request)
        {
            ServiceLocator.CommandBus.Send(new DelAddressCommand(Guid.NewGuid(), request.Id));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AddressListResponse GetAddressList(string accountId)
        {
            var list = ServiceLocator.AddressDatabase.GetAddressList(accountId);
            var result = new AddressListResponse();
            result.Status = 10001;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var address = new Address();
                    address.AccountId = item.AccountId;
                    address.ContactAddress = item.Address;
                    address.Id = item.AddressId;
                    address.DistrictId = item.DistrictId;
                    address.DistrictName = item.DistrictName!= null ? item.DistrictName:string.Empty;
                    address.SchoolId = item.SchoolId;
                    address.SchoolName = item.SchoolName != null ? item.SchoolName : string.Empty;
                    address.ContactMobile = item.Mobile;
                    address.ContactName = item.UserName;
                    address.Gender = item.Gender;
                    address.IsDefault = item.IsDefault;
                    address.DormId = item.DormId;
                    address.DormName = item.DormName != null? item.DormName : string.Empty;
                    result.AddressList.Add(address);
                }
            }
            return result;
        }
        public static RegionListResponse GetRegionDataList(int dataType)
        {
            var list = ServiceLocator.AddressDatabase.GetRegionDataList(dataType);
            var result = new RegionListResponse();
            result.Status = 10001;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var region = new RegionData();
                    region.DataId = item.DataID;
                    region.DataName = item.DataName;
                    region.ParentDataId = item.ParentDataID;
                    result.RegionList.Add(region);
                }
            }
            return result;
        }
        public static RegionDataResponse GetRegionData(int dataId)
        {
            var domain = ServiceLocator.AddressDatabase.GetRegionData(dataId);
            var result = new RegionDataResponse();
            result.Status = 10001;
            if (result != null)
            {
                result.DataId = domain.DataID;
                result.DataName = domain.DataName;
                result.ParentDataId = domain.ParentDataID;
                result.CityId = domain.CityID;
                result.ProvinceId = domain.ProvinceID;
            }
            return result;
        }
        public static RegionListResponse GetChildRegionData(int dataId, long updateTime)
        {
            List<RegionDataDomain> list = new List<RegionDataDomain>();
            if (updateTime == 0)
            {
                list = ServiceLocator.AddressDatabase.GetChildRegionData(dataId);
            }
            else
            {
                list = ServiceLocator.AddressDatabase.GetChildRegionData(dataId, new DateTime(updateTime));
            }
            var result = new RegionListResponse();
            result.Status = 10001;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var region = new RegionData();
                    region.DataId = item.DataID;
                    region.DataName = item.DataName;
                    region.ParentDataId = item.ParentDataID;
                    region.UpdateTime = item.UpdateTime.Ticks;
                    result.RegionList.Add(region);
                }
            }
            return result;
        }

        public static RegionListResponse GetSelectedRegionDataList(string accountId)
        {
            var list = ServiceLocator.AddressDatabase.GetSelectedRegionDataList(accountId);
            var result = new RegionListResponse();
            result.Status = 10001;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var region = new RegionData();
                    region.DataId = item.DataID;
                    region.DataName = item.DataName;
                    region.ParentDataId = item.ParentDataID;
                    result.RegionList.Add(region);
                }
            }
            return result;
        }

        public static AddressResponse GetDefaultSelectedAddress(string accountId)
        {
            var domain = ServiceLocator.AddressDatabase.GetDefaultSelectedAddress(accountId);
            var result = new AddressResponse();
            result.Status = 10002;
            if (domain != null && !string.IsNullOrEmpty(domain.AccountId) )
            {
                var address = new Address();
                address.AccountId = domain.AccountId;     
                address.Id = domain.AddressId;
                address.DistrictId = domain.DistrictId;
                address.DistrictName = domain.DistrictName!= null ? domain.DistrictName:string.Empty;
                address.SchoolId = domain.SchoolId;
                address.SchoolName = domain.SchoolName!= null? domain.SchoolName:string.Empty;
                address.ContactMobile = domain.Mobile;
                address.ContactName = domain.UserName;
                address.Gender = domain.Gender;
                address.BuildingId = domain.BuildingId;
                address.BuildingName = domain.BuildingName != null ? domain.BuildingName : string.Empty;
                address.DormId = domain.DormId;
                address.DormName = domain.DormName != null ? domain.DormName : string.Empty;
                address.ContactAddress = domain.Address;
                result.Address = address;
                result.Status = 10001;
            }
            return result;
        }

        public static RegionListResponse GetChildRegionDataList(int dataId,long updateTime)
        {
            List<RegionDataDomain> list = new List<RegionDataDomain>();
            if (updateTime == 0)
            {
                list = ServiceLocator.AddressDatabase.GetChildRegionDataList(dataId);
            }
            else
            {
                System.Console.WriteLine("updateTime:"+ (new DateTime(updateTime)).ToString("yyyy-MM-dd HH24:mm:ss"));
                list = ServiceLocator.AddressDatabase.GetChildRegionDataList(dataId,new DateTime(updateTime));
            }
            var result = new RegionListResponse();
            result.Status = 10001;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var region = new RegionData();
                    region.DataId = item.DataID;
                    region.DataName = item.DataName;
                    region.ParentDataId = item.ParentDataID;
                    region.UpdateTime = item.UpdateTime.Ticks;
                    result.RegionList.Add(region);
                }
            }
            return result;
        }
    }
}
