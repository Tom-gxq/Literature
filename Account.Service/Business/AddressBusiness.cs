using SP.Service;
using SP.Service.Domain.Commands.Account;
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
                , request.Address.ContactMobile, request.Address.DistrictId, request.Address.ContactAddress, request.Address.AccountId, request.Address.Dorm,request.Address.IsDefault));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AccountResultResponse EditAccountAddress(AddressRequest request)
        {
            ServiceLocator.CommandBus.Send(new EditAddressCommand(Guid.NewGuid(), request.Address.Id, request.Address.ContactName, request.Address.Gender
                , request.Address.ContactMobile, request.Address.DistrictId, request.Address.ContactAddress, request.Address.AccountId, request.Address.Dorm,request.Address.IsDefault));
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
                    address.Dorm = item.Dorm!= null? item.Dorm:string.Empty;
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
        public static RegionListResponse GetChildRegionData(int dataId)
        {
            var list = ServiceLocator.AddressDatabase.GetChildRegionData(dataId);
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
            var list = ServiceLocator.AddressDatabase.GetDefaultSelectedAddress(accountId);
            var result = new AddressResponse();
            result.Status = 10001;
            if (list != null && list.Count > 0)
            {
                var address = new Address();
                address.AccountId = list[0].AccountId;
                address.ContactAddress = list[0].Address;
                address.Id = list[0].AddressId;
                address.DistrictId = list[0].DistrictId;
                address.DistrictName = list[0].DistrictName!= null ? list[0].DistrictName:string.Empty;
                address.SchoolId = list[0].SchoolId;
                address.SchoolName = list[0].SchoolName!= null? list[0].SchoolName:string.Empty;
                address.ContactMobile = list[0].Mobile;
                address.ContactName = list[0].UserName;
                address.Gender = list[0].Gender;
                address.Dorm = list[0].Dorm!= null? list[0].Dorm:string.Empty;
                result.Address = address;
            }
            return result;
        }

        public static RegionListResponse GetChildRegionDataList(int dataId)
        {
            var list = ServiceLocator.AddressDatabase.GetChildRegionDataList(dataId);
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
    }
}
