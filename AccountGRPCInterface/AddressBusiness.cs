using SP.Api.Model.Account;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;
using static SP.Service.AccountService;

namespace AccountGRPCInterface
{
    public class AddressBusiness
    {
        public static List<AddressModel> GetAddressList(string accountId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                 AccountId = accountId
            };
            var result = client.GetAddressList(request1);
            var list = new List<AddressModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.AddressList)
                {
                    var domain = new AddressModel();
                    domain.id = item.Id;
                    domain.districtId = item.DistrictId;
                    domain.districtName = item.DistrictName;
                    domain.schoolId = item.SchoolId;
                    domain.schoolName = item.SchoolName;
                    domain.status = item.Status;
                    domain.gender = item.Gender == 1 ? true : false;
                    domain.contactAddress = item.ContactAddress;
                    domain.contactMobile = item.ContactMobile;
                    domain.contactName = item.ContactName;
                    domain.accountId = item.AccountId;
                    domain.dormId = item.DormId;
                    domain.dorm = item.DormName;
                    domain.isDefault = item.IsDefault == 1 ? true : false;
                    list.Add(domain);
                }
            }
            return list;
        }
        public static AddressModel GetDefaultSelectedAddress(string accountId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId
            };
            var result = client.GetDefaultSelectedAddress(request1);
            var domain = new AddressModel();
            if (result.Status == 10001)
            {
                domain.id = result.Address.Id;
                domain.districtId = result.Address.DistrictId;
                domain.districtName = result.Address.DistrictName;
                domain.schoolId = result.Address.SchoolId;
                domain.schoolName = result.Address.SchoolName;
                domain.status = result.Address.Status;
                domain.gender = result.Address.Gender == 1 ? true : false;
                domain.contactAddress = result.Address.ContactAddress;
                domain.contactMobile = result.Address.ContactMobile;
                domain.contactName = result.Address.ContactName;
                domain.buildinglId = result.Address.BuildingId;
                domain.buildingName = result.Address.BuildingName;
                domain.dormId = result.Address.DormId;
                domain.dorm = result.Address.DormName;
            }
            return domain;
        }

        public static List<RegionDataModel> GetRegionDataList(int dataType)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new RegionDataRequest()
            {
                 DataType= dataType
            };
            var result = client.GetRegionDataList(request1);
            var list = new List<RegionDataModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.RegionList)
                {
                    var domain = new RegionDataModel();
                    domain.dataId = item.DataId;
                    domain.dataName = item.DataName;
                    domain.parentDataId = item.ParentDataId;

                    list.Add(domain);
                }
            }
            return list;
        }

        public static RegionDataModel GetRegionData(int dataId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new RegionIDRequest()
            {
                DataId= dataId
            };
            var result = client.GetRegionData(request1);
            var domain = new RegionDataModel();
            if (result.Status == 10001)
            {               
                domain.dataId = result.DataId;
                domain.dataName = result.DataName;
                domain.parentDataId = result.ParentDataId;
                
            }
            return domain;
        }
        public static List<RegionDataModel> GetChildRegionData(int dataId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new RegionIDRequest()
            {
                DataId = dataId
            };
            var result = client.GetChildRegionData(request1);
            var list = new List<RegionDataModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.RegionList)
                {
                    var domain = new RegionDataModel();
                    domain.dataId = item.DataId;
                    domain.dataName = item.DataName;
                    domain.parentDataId = item.ParentDataId;

                    list.Add(domain);
                }
            }
            return list;
        }

        public static bool AddAccountAddress(AddressModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AddressRequest()
            {
                Address = new Address()
                {
                    AccountId = model.accountId,
                    ContactAddress = model.contactAddress!= null? model.contactAddress:string.Empty,
                    ContactMobile = model.contactMobile,
                    ContactName = model.contactName,
                    Gender = model.gender?1:0,
                    DistrictId = model.dormId,
                }
            };
            bool ret = false;
            var result = client.AddAccountAddress(request1);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }

        public static bool EditAccountAddress(AddressModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AddressRequest()
            {
                Address = new Address()
                {
                    Id = model.id,
                    AccountId = model.accountId,
                    ContactAddress = !string.IsNullOrEmpty(model.contactAddress)? model.contactAddress:string.Empty,
                    ContactMobile = !string.IsNullOrEmpty(model.contactMobile) ? model.contactMobile : string.Empty,
                    ContactName = !string.IsNullOrEmpty(model.contactName) ? model.contactName : string.Empty,
                    Gender = model.gender ? 1 : 0,
                    DistrictId = model.dormId,
                    IsDefault = model.isDefault ?1:0
                }
            };
            bool ret = false;
            var result = client.EditAccountAddress(request1);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }
        public static bool UpdateAddressStatus(int id,int status,string accountId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AddressStatusRequest()
            {
                AccountId = accountId,
                Id = id,
                Status = status
            };
            bool ret = false;
            var result = client.UpdateAddressStatus(request1);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }
        public static bool UpdateAddressDorm(int id, int dormId, string accountId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AddressDormRequest()
            {
                AccountId = accountId,
                Id = id,
                DormId = dormId
            };
            bool ret = false;
            var result = client.UpdateAddressDorm(request1);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }

        public static bool DelAddress(int id)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new DelAddressRequest()
            {
                Id = id,
            };
            bool ret = false;
            var result = client.DelAddress(request1);
            if (result.Status == 10001)
            {
                ret = true;
            }
            return ret;
        }

        public static List<RegionDataModel> GetChildRegionDataList(int parentDataId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new RegionIDRequest()
            {
                DataId = parentDataId
            };
            var result = client.GetChildRegionDataList(request1);
            var list = new List<RegionDataModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.RegionList)
                {
                    var domain = new RegionDataModel();
                    domain.dataId = item.DataId;
                    domain.dataName = item.DataName;
                    domain.parentDataId = item.ParentDataId;

                    list.Add(domain);
                }
            }
            return list;
        }

        public static RegionDataModel GetDefaultRegionData(string accountId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId
            };
            var result = client.GetSelectedRegionDataList(request1);
            RegionDataModel model = new RegionDataModel();
            if (result.Status == 10001)
            {
                if (result.RegionList != null && result.RegionList.Count > 0)
                {
                    model.dataId = result.RegionList[0].DataId;
                    model.dataName = result.RegionList[0].DataName;
                    model.parentDataId = result.RegionList[0].ParentDataId;
                }
                if(model.dataId > 0)
                {
                    var requst2 = new RegionIDRequest()
                    {
                        DataId = model.dataId
                    };
                    model.childList = new List<RegionDataModel>();
                    var childResult = client.GetChildRegionData(requst2);
                    if(childResult?.RegionList != null)
                    {
                        foreach (var item in childResult.RegionList)
                        {
                            RegionDataModel child = new RegionDataModel();
                            child.dataId = item.DataId;
                            child.dataName = item.DataName;
                            child.parentDataId = item.ParentDataId;
                            model.childList.Add(child);
                        }
                    }
                }
            }
            return model;
        }

        public static List<RegionDataModel> GetSchoolDistrictList(int dataId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new RegionIDRequest()
            {
                DataId = dataId
            };
            var result = client.GetChildRegionDataList(request1);
            var list = new List<RegionDataModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.RegionList)
                {
                    var domain = new RegionDataModel();
                    domain.dataId = item.DataId;
                    domain.dataName = item.DataName;
                    domain.parentDataId = item.ParentDataId;
                    if (domain.dataId > 0)
                    {
                        GetChildRegion(domain, client);
                    }
                    list.Add(domain);
                }
            }
            return list;
        }
        private static void GetChildRegion(RegionDataModel model, AccountServiceClient client)
        {
            var requst2 = new RegionIDRequest()
            {
                DataId = model.dataId
            };
            model.childList = new List<RegionDataModel>();
            var childResult = client.GetChildRegionData(requst2);
            if (childResult?.RegionList != null)
            {
                foreach (var item in childResult.RegionList)
                {
                    RegionDataModel child = new RegionDataModel();
                    child.dataId = item.DataId;
                    child.dataName = item.DataName;
                    child.parentDataId = item.ParentDataId;
                    if(child.parentDataId > 0)
                    {
                        GetChildRegion(child, client);
                    }
                    model.childList.Add(child);
                }
            }
        }
    }
}
