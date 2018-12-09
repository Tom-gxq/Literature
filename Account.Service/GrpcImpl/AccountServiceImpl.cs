using Account.Service.Business;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SP.Service;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SP.Service.AccountService;

namespace Account.Service.GrpcImpl
{
    public class AccountServiceImpl: AccountServiceBase
    {
        private ILogger logger = new ServiceCollection()
              .AddLogging()
              .BuildServiceProvider()
              .GetService<ILoggerFactory>()
              .AddConsole()
              .CreateLogger("AccountService");

        private int prjLicEID = 7000;

        public AccountServiceImpl(int port)
        {
            if (port > 0)
            {
                this.prjLicEID = port;
            }
        }

        public override Task<AccountResultResponse> RegistAccount(RegistRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} RegistAccount Connected! MobilePhone:[{MobilePhone}]", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.MobilePhone ?? string.Empty);
            AccountResultResponse response = null;
            try
            {
                 response = AccountBusiness.RegistAccount(request.Email, request.MobilePhone, request.PassWord, request.UserName);
            }
            catch(Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "RegistAccount Exception");
            }
            return Task.FromResult(response);
        }
        public override Task<AccountResultResponse> UpdateAccount(UpdateAccountRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAccount Connected! MobilePhone:[{MobilePhone}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.MobilePhone ?? string.Empty);
            AccountResultResponse response = null;
            try
            {
                response = AccountBusiness.UpdateAccount(request.Email, request.MobilePhone, request.PassWord, request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAccount Exception");
            }
            return Task.FromResult(response);
        }
        /**
	    * 验证登陆密码
	    */
        public override Task<AccountResultResponse> ValidateLogin(LoginRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} ValidateLogin Connected! UserName:[{UserName}]", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.UserName ?? string.Empty);
            AccountResultResponse response = null;
            try
            {
                response = AccountBusiness.ValidateLogin(request.UserName, request.PassWord);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "ValidateLogin Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<AccountResponse> GetAccountDetail(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAccountDetail Connected! AccountId:[{AccountId}]", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            AccountResponse response = new AccountResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetAccountDetail(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAccountDetail Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAccountDetail {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<AccountResponse> GetAccount(AccountRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAccount Connected! Account:[{Account}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Account ?? string.Empty);
            AccountResponse response = new AccountResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetAccount(request.Account);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAccount Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAccount {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        /**
	    * 获取AccessToken
	    */
        public override Task<AccessTokenResponse> GetAccessToken(TokenKeyRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAccessToken Connected! AccessToken:[{AccessToken}]", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Key ?? string.Empty);
            AccessTokenResponse response = null;
            try
            {
                response = AccountBusiness.GetAccessToken(request.Key);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAccessToken Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAccessToken {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<AccessTokenResponse> GetAccessTokenByRefreshToken(TokenKeyRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAccessTokenByRefreshToken Connected! AccessToken:[{AccessToken}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Key ?? string.Empty);
            AccessTokenResponse response = null;
            try
            {
                response = AccountBusiness.GetAccessTokenByRefreshToken(request.Key);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAccessTokenByRefreshToken Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAccessTokenByRefreshToken {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        /**
        * 添加AccessToken
        */
        public override Task<AccessTokenResponse> AddAccessToken(AccessTokenRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} AddAccessToken Connected! AccountId:[{AccountId}] AccessToken:[{AccessToken}]", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(),
                request.AccessToken.AccountId ?? string.Empty, request.AccessToken.AccessToken_ );
            AccessTokenResponse response = new AccessTokenResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.AddAccessToken(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddAccessToken Exception");
            }
            return Task.FromResult(response);
        }

        /**
        * 删除AccessToken
        */
        public override Task<AccessTokenResponse> RemoveAccessToken(TokenIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} RemoveAccessToken Connected! AccountId:[{AccountId}] AccessToken:[{AccessToken}]", 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty, request.AccessToken ?? string.Empty);
            AccessTokenResponse response = new AccessTokenResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.RemoveAccessToken(request.AccessToken, request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "RemoveAccessToken Exception");
            }
            return Task.FromResult(response);
        }

        /**
	    * 添加用户地址
	    */
        public override Task<AccountResultResponse> AddAccountAddress(AddressRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} AddAccountAddress Connected! AccountId:[{AccountId}] ContactMobile:[{ContactMobile}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Address.AccountId ?? string.Empty, request.Address.ContactMobile ?? string.Empty);
            AccountResultResponse response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.AddAccountAddress(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddAccountAddress Exception");
            }
            return Task.FromResult(response);
        }

        /**
        * 编辑用户地址
        */
        public override Task<AccountResultResponse> EditAccountAddress(AddressRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} EditAccountAddress Connected! Id:[{Id}] ContactMobile:[{ContactMobile}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Address.Id , request.Address.ContactMobile ?? string.Empty);
            AccountResultResponse response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.EditAccountAddress(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "EditAccountAddress Exception");
            }
            return Task.FromResult(response);
        }

        /**
        * 更新用户地址状态
        */
        public override Task<AccountResultResponse> UpdateAddressStatus(AddressStatusRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAddressStatus Connected! Id:[{Id}] Status:[{Status}] AccountId:[{AccountId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Id, request.Status, request.AccountId);
            AccountResultResponse response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.UpdateAddressStatus(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAddressStatus Exception");
            }
            return Task.FromResult(response);
        }

        /**
        * 更新用户地址
        */
        public override Task<AccountResultResponse> UpdateAddressDorm(AddressDormRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAddressDorm Connected! Id:[{Id}] DormId:[{DormId}] AccountId:[{AccountId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Id, request.DormId, request.AccountId);
            AccountResultResponse response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.UpdateAddressDorm(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAddressDorm Exception");
            }
            return Task.FromResult(response);
        }

        /**
        * 获取用户地址列表
        */
        public override Task<AddressListResponse> GetAddressList(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAddressList Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            AddressListResponse response = new AddressListResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.GetAddressList(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAddressList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAddressList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        /**
        * 删除用户地址
        */
        public override Task<AccountResultResponse> DelAddress(DelAddressRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} DelAddress Connected! Id:[{Id}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Id);
            AccountResultResponse response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.DelAddress(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "DelAddress Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<RegionListResponse> GetRegionDataList(RegionDataRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetRegionDataList Connected! DataType:[{DataType}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.DataType);
            RegionListResponse response = new RegionListResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.GetRegionDataList(request.DataType);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetRegionDataList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetRegionDataList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<RegionDataResponse> GetRegionData(RegionIDRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetRegionData Connected! DataId:[{DataId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.DataId);
            RegionDataResponse response = new RegionDataResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.GetRegionData(request.DataId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetRegionData Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetRegionData {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<RegionListResponse> GetChildRegionData(ChildRegionRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetChildRegionData Connected! RegionID:[{RegionID}] UpdateTime:[{UpdateTime}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.DataId, request.UpdateTime);
            RegionListResponse response = new RegionListResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.GetChildRegionData(request.DataId, request.UpdateTime);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetChildRegionData Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetChildRegionData {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ShoppingCartResultResponse> AddShoppingCart(ShoppingCartRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} AddShoppingCart Connected! AccountId:[{AccountId}] ShopId:[{ShopId}] Quantity:[{Quantity}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId, request.ShopId, request.ProductId,request.Quantity);
            ShoppingCartResultResponse response = new ShoppingCartResultResponse();
            response.Status = 10002;
            try
            {
                response = ShoppingCartBusiness.AddShoppingCart(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddShoppingCart Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<ShoppingCartListResponse> GetMyShoppingCartList(GetShoppingCartRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetMyShoppingCartList Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            ShoppingCartListResponse response = new ShoppingCartListResponse();
            response.Status = 10002;
            try
            {
                response = ShoppingCartBusiness.GetMyShoppingCartList(request.AccountId, request.UserType);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetMyShoppingCartList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetMyShoppingCartList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<ShoppingCartListResponse> GetMyShoppingCartListByOrderId(MyShoppingCartRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetMyShoppingCartListByOrderId Connected! AccountId:[{AccountId}] OrderId:[{OrderId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId, request.OrderId);
            ShoppingCartListResponse response = new ShoppingCartListResponse();
            response.Status = 10002;
            try
            {
                response = ShoppingCartBusiness.GetMyShoppingCartListByOrderId(request.AccountId, request.OrderId, request.UserType);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetMyShoppingCartListByOrderId Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetMyShoppingCartListByOrderId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ShoppingCartCountResponse> GetMyShoppingCartCount(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetMyShoppingCartCount Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            ShoppingCartCountResponse response = new ShoppingCartCountResponse();
            response.Status = 10002;
            try
            {
                response.Count = ShoppingCartBusiness.GetMyShoppingCartCount(request.AccountId);
                response.Status = 10001;
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetMyShoppingCartCount Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetMyShoppingCartCount {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ShoppingCartResultResponse> UpdateShoppingCartQuantity(ShoppingCartQuantityRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateShoppingCartQuantity Connected! CartId:[{CartId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.CartId);
            ShoppingCartResultResponse response = new ShoppingCartResultResponse();
            response.Status = 10002;
            try
            {
                var result = ShoppingCartBusiness.UpdateShoppingCartQuantity(request.CartId, request.Quantity);
                if(result)
                {
                    response.Status = 10001;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateShoppingCartQuantity Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateShoppingCartQuantity {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ShoppingCartResultResponse> UpdateShoppingCartEnabled(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateShoppingCartEnabled Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            ShoppingCartResultResponse response = new ShoppingCartResultResponse();
            response.Status = 10002;
            try
            {
                var result = ShoppingCartBusiness.UpdateShoppingCartEnabled(request.AccountId);
                response.Status = 10001;
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateShoppingCartEnabled Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateShoppingCartEnabled {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ShoppingCartResultResponse> UpdateShoppingCartOrderId(ShoppingCartOrderIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateShoppingCartOrderId Connected! OrderId:[{OrderId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.OrderId);
            ShoppingCartResultResponse response = new ShoppingCartResultResponse();
            response.Status = 10002;
            try
            {
                List<string> list = new List<string>();
                for (int i =0;i< request.CartId.Count;i++)
                {
                    list.Add(request.CartId[i]);
                }
                var result = ShoppingCartBusiness.UpdateShoppingCartOrderId(request.OrderId, list);
                if (result)
                {
                    response.Status = 10001;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateShoppingCartOrderId Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateShoppingCartOrderId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<RegionListResponse> GetSelectedRegionDataList(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetSelectedRegionDataList Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            RegionListResponse response = new RegionListResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.GetSelectedRegionDataList(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSelectedRegionDataList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSelectedRegionDataList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        /**
       * 获取用户地址列表
       */
        public override Task<AddressResponse> GetDefaultSelectedAddress(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetDefaultSelectedAddress Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            var response = new AddressResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.GetDefaultSelectedAddress(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetDefaultSelectedAddress Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetDefaultSelectedAddress {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<RegionListResponse> GetChildRegionDataList(ChildRegionRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetChildRegionDataList Connected! DataId:[{DataId}]  UpdateTime:[{UpdateTime}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.DataId, request.UpdateTime);
            RegionListResponse response = new RegionListResponse();
            response.Status = 10002;
            try
            {
                response = AddressBusiness.GetChildRegionDataList(request.DataId, request.UpdateTime);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetChildRegionDataList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetChildRegionDataList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> AddAccountAuthentication(AddAuthenticationRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} AddAccountAuthentication Connected! Account:[{Account}] Token:[{Token}] VerifyCode:[{VerifyCode}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountAuth.Account,
                request.AccountAuth.Token, request.AccountAuth.VerifyCode);
            var response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.AddAccountAuthentication(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddAccountAuthentication Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> UpdateAuthStatusByAccount(UpdateAuthenticationRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAuthStatusByAccount Connected! Account:[{Account}] Status:[{Status}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Account,
                request.Status);
            var response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.UpdateAuthStatusByAccount(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAuthStatusByAccount Exception");
            }

            return Task.FromResult(response);
        }
        public override Task<VerifyCodeResponse> GetValidVerifyCodeByAccount(GetVerifyCodeRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetValidVerifyCodeByAccount Connected! Account:[{Account}] AuthType:[{AuthType}] AccountId:[{AccountId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Account,
                request.AuthType, request.AccountId);
            var response = new VerifyCodeResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetValidVerifyCodeByAccount(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetValidVerifyCodeByAccount Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetValidVerifyCodeByAccount {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<AssociatorResponse> AddAssociator(AddAssociatorRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} AddAssociator Connected! AccountId:[{AccountId}]  KindId:[{KindId}]  Quantity:[{Quantity}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId, request.KindId, request.Quantity);
            var response = new AssociatorResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.AddAssociator(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddAssociator Exception");
            }
            return Task.FromResult(response);
        }
        public override Task<AccountResultResponse> UpdateAssociatorStatus(UpdateAssociatorStatusRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAssociatorStatus Connected! AssociatorId:[{AssociatorId}] Status:[{Status}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AssociatorId, request.Status);
            var response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.UpdateAssociatorStatus(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAssociatorStatus Exception");
            }
            return Task.FromResult(response);
        }
        public override Task<AssociatorListResponse> GetAssociatorByAccountId(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAssociatorByAccountId Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            var response = new AssociatorListResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetAssociatorByAccountId(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAssociatorByAccountId Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAssociatorByAccountId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<AssociatorListResponse> GetDiscountByAccountId(DiscountRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetDiscountByAccountId Connected! AccountId:[{AccountId}] Kind:[{Kind}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId, request.Kind);
            var response = new AssociatorListResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetDiscountByAccountId(request.AccountId, request.Kind);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetDiscountByAccountId Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetDiscountByAccountId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<SysKindListResponse> GetSysKindList(GetSysKindRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetSysKindList Connected! Kind:[{Kind}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Kind);
            var response = new SysKindListResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetSysKindList(request.Kind);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSysKindList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSysKindList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<AccountFullInfoResponse> GetAccountFullInfo(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAccountFullInfo Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            var response = new AccountFullInfoResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetAccountFullInfo(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAccountFullInfo Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAccountFullInfo {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<AccountResultResponse> UpdateAccountFullInfo(AccountFullInfoRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAccountFullInfo Connected! AccountId:[{AccountId}] ",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            var response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.UpdateAccountFullInfo(request);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAccountFullInfo Exception");
            }
            return Task.FromResult(response);
        }

        /**
        * 更新用户地址状态
        */
        public override Task<AssociatorResponse> GetAssociatorById(AssociatorIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAssociatorById Connected!  AssociatorId:[{AssociatorId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AssociatorId);
            var response = new AssociatorResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetAssociatorById(request.AssociatorId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAssociatorById Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAssociatorById Result:[{Result}] ", response.ToString());
            return Task.FromResult(response);
        }

        public override Task<AssociatorResponse> GetAssociatorByCode(AssociatorCodeRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetAssociatorByCode Connected!  AssociatorCode:[{AssociatorCode}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AssociatorCode);
            var response = new AssociatorResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetAssociatorByCode(request.AssociatorCode);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAssociatorByCode Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAssociatorByCode Result:[{Result}] ", response.ToString());
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> SetAccountPayPwd(AccountPayPwdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} SetAccountPayPwd Connected! AccountId:[{AccountId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            AccountResultResponse response = null;
            try
            {
                response = AccountBusiness.SetAccountPayPwd(request.AccountId, request.PayPwd);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "SetAccountPayPwd Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> UpdateAccountPayPwd(AccountPayPwdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAccountPayPwd Connected! AccountId:[{AccountId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            AccountResultResponse response = null;
            try
            {
                response = AccountBusiness.UpdateAccountPayPwd(request.AccountId, request.PayPwd);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAccountPayPwd Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> UpdateAccountLoginPwd(AccountPayPwdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAccountLoginPwd Connected! AccountId:[{AccountId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            AccountResultResponse response = null;
            try
            {
                response = AccountBusiness.UpdateAccountLoginPwd(request.AccountId, request.PayPwd);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAccountLoginPwd Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> UpdateAccountMobile(AccountMobileRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAccountMobile Connected! AccountId:[{AccountId}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty);
            AccountResultResponse response = null;
            try
            {
                response = AccountBusiness.UpdateAccountMobile(request.AccountId, request.MobilePhone);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAccountMobile Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> BindOtherAccount(BingAccountRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} BindOtherAccount Connected! AccountId:[{AccountId}] OtherAccount:[{OtherAccount}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId ?? string.Empty, request.OtherAccount ?? string.Empty);
            AccountResultResponse response = null;
            try
            {
                response = AccountBusiness.BindOtherAccount(request.AccountId, request.OtherType, request.OtherAccount);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "BindOtherAccount Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> CreateOtherAccount(OtherAccountRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} CreateOtherAccount Connected! MobilePhone:[{MobilePhone}] OtherAccount:[{OtherAccount}] OtherType:[{OtherType}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.MobilePhone ?? string.Empty, request.OtherAccount ?? string.Empty, request.OtherType);
            AccountResultResponse response = null;
            try
            {
                response = AccountBusiness.CreateOtherAccount(request.MobilePhone, request.OtherType, request.OtherAccount, request.FullName, request.Avatar, request.Gender);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "CreateOtherAccount Exception");
            }
            return Task.FromResult(response);
        }

        public override Task<AccountResponse> GetOtherAccount(GetOtherAccountRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetOtherAccount Connected! OtherAccount:[{OtherAccount}]",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.OtherAccount ?? string.Empty);
            AccountResponse response = new AccountResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetOtherAccount(request.OtherAccount, (OtherType)request.OtherType);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetOtherAccount Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetOtherAccount {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> UpdateAccountIDInfo(AccountIDRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateAccountIDInfo Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} {DormId} {FullName} {UserType} ",
                request.AccountId, request.DormId, request.FullName, request.UserType);
            AccountResultResponse response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.UpdateAccountIDInfo(request.AccountId, request.DormId, request.FullName, request.UserType);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateAccountIDInfo Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateAccountIDInfo {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<AccountResultResponse> ApplyPartner(ApplyPartnerRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} ApplyPartner Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} {DormId} ", request.AccountId, request.DormId);
            AccountResultResponse response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.ApplyPartner(request.AccountId, request.DormId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "ApplyPartner Exception");
            }
            logger.LogInformation(this.prjLicEID, "ApplyPartner {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<AccountResultResponse> BalancePay(BalancePayRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} BalancePay Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} {OrderCode} {PassWord} {Token} {Amount}", request.AccountId, request.OrderCode, request.PassWord, request.Token, request.Amount);
            AccountResultResponse response = new AccountResultResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.BalancePay(request.AccountId, request.Token, request.PassWord, request.Amount, request.OrderCode, request.Sign);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "BalancePay Exception");
            }
            logger.LogInformation(this.prjLicEID, "BalancePay {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<TradeListResponse> GetTradeList(TradeListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetTradeList Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} {PageIndex} {PageSize}", request.AccountId,request.PageIndex, request.PageSize);
            var response = new TradeListResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetTradeList(request.AccountId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetTradeList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetTradeList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<TradeTotalResponse> GetTradeListCount(AccountIDRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} GetTradeListCount Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} ", request.AccountId);
            var response = new TradeTotalResponse();
            response.Status = 10002;
            try
            {
                response = AccountBusiness.GetTradeListCount(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetTradeListCount Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetTradeListCount {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
    }
}
