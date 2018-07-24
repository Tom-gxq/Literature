using SP.Service;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Service.Business
{
    public class AccountBusiness
    {
        //public static void AddAccount(AddOrderRequest request)
        //{
        //    var cartIds = new List<string>();
        //    foreach (var item in request.CartIds)
        //    {
        //        cartIds.Add(item);
        //    }
        //    ServiceLocator.CommandBus.Send(new CreatAccountCommand(Guid.NewGuid(), request.Remark, request.AccountId, cartIds));
        //}

        public static AccountResultResponse ValidateLogin(string userName,string passWord)
        {
            var result= ServiceLocator.ReportDatabase.ValidateLogin(userName, passWord);
            var response = new AccountResultResponse();
            response.Status = result ? 10001 : 10002;
            return response;
        }
        public static AccountResultResponse RegistAccount(string email,string mobilePhone, string passWord, string userName)
        {
            ServiceLocator.CommandBus.Send(new CreatAccountCommand(Guid.NewGuid(), mobilePhone,email, passWord,1, userName));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }
        public static AccountResultResponse UpdateAccount(string email, string mobilePhone, string passWord, string accountId)
        {
            ServiceLocator.CommandBus.Send(new EditAccountCommand(new Guid(accountId), mobilePhone, email, passWord));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }
        public static AccountResponse GetAccountDetail(string accountId)
        {
            var account = ServiceLocator.ReportDatabase.GetAccountById(accountId);
            var result = new AccountResponse();
            result.Status = 10001;
            if (account != null)
            {
                result.MobilePhone = account.MobilePhone;
                result.Email = account.Email;
                result.Password = account.Password;
                result.AccountId = account.AccountId;
                result.AliBind = !string.IsNullOrEmpty(account.AliBind) ? account.AliBind : string.Empty;
                result.WxBind = !string.IsNullOrEmpty(account.WxBind) ? account.WxBind : string.Empty;
                result.QQBind = !string.IsNullOrEmpty(account.QQBind) ? account.QQBind : string.Empty;
            }
            return result;
        }

        public static AccountResponse GetAccount(string account)
        {
            var domain = ServiceLocator.ReportDatabase.GetAccount(account);
            var result = new AccountResponse();
            result.Status = 10001;
            if (domain != null)
            {
                result.MobilePhone = !string.IsNullOrEmpty(domain.MobilePhone)? domain.MobilePhone:string.Empty;
                result.Email = !string.IsNullOrEmpty(domain.Email) ? domain.Email : string.Empty;
                result.Password = !string.IsNullOrEmpty(domain.Password) ? domain.Password : string.Empty;
                result.AccountId = !string.IsNullOrEmpty(domain.AccountId) ? domain.AccountId : string.Empty;
                result.AliBind = !string.IsNullOrEmpty(domain.AliBind) ? domain.AliBind : string.Empty;
                result.WxBind = !string.IsNullOrEmpty(domain.WxBind) ? domain.WxBind : string.Empty;
                result.QQBind = !string.IsNullOrEmpty(domain.QQBind) ? domain.QQBind : string.Empty;
            }
            return result;
        }
        public static AccessTokenResponse GetAccessToken(string userKey)
        {
            var account = ServiceLocator.ReportDatabase.GetAccessToken(userKey);
            var result = new AccessTokenResponse();
            result.Status = 10001;
            result.AccessToken = new AccessToken();
            result.AccessToken.AccountId = account.AccountId;
            result.AccessToken.AccessToken_ = account.AccessToken;
            result.AccessToken.AccessTokenExpires = account.AccessTokenExpires.Ticks;
            result.AccessToken.CreateTime = account.CreateTime.Ticks;
            result.AccessToken.RefreshToken = account.RefreshToken;
            result.AccessToken.RefreshTokenExpires = account.RefreshTokenExpires.Ticks;
            return result;
        }

        public static AccessTokenResponse GetAccessTokenByRefreshToken(string userKey)
        {
            var account = ServiceLocator.ReportDatabase.GetAccessTokenByRefreshToken(userKey);
            var result = new AccessTokenResponse();
            if (account != null)
            {
                result.Status = 10001;
                result.AccessToken = new AccessToken();
                result.AccessToken.AccountId = account.AccountId;
                result.AccessToken.AccessToken_ = account.AccessToken;
                result.AccessToken.AccessTokenExpires = account.AccessTokenExpires.Ticks;
                result.AccessToken.CreateTime = account.CreateTime.Ticks;
                result.AccessToken.RefreshToken = account.RefreshToken;
                result.AccessToken.RefreshTokenExpires = account.RefreshTokenExpires.Ticks;
            }
            else
            {
                result.Status = 10003;
            }
            return result;
        }

        public static AccessTokenResponse AddAccessToken(AccessTokenRequest request)
        {
            ServiceLocator.CommandBus.Send(new CreateAccessTokenCommand(Guid.NewGuid(), request.AccessToken.AccessToken_, 
                request.AccessToken.AccountId, new DateTime(request.AccessToken.AccessTokenExpires),
                request.AccessToken.RefreshToken,new DateTime(request.AccessToken.RefreshTokenExpires)));
            var result = new AccessTokenResponse();
            result.Status = 10001;
            return result;
        }

        public static AccessTokenResponse RemoveAccessToken(string accessToken,string accountId)
        {
            ServiceLocator.CommandBus.Send(new DelAccessTokenCommand(Guid.NewGuid(), accessToken, accountId));
            var result = new AccessTokenResponse();
            result.Status = 10001;
            return result;
        }

        public static AccountResultResponse AddAccountAuthentication(AddAuthenticationRequest request)
        {
            ServiceLocator.CommandBus.Send(new CreatAuthenticationCommand(Guid.NewGuid(), request.AccountAuth.AuthType, request.AccountAuth.AccountId
                , request.AccountAuth.Account, request.AccountAuth.VerifyCode, request.AccountAuth.Token));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AccountResultResponse UpdateAuthStatusByAccount(UpdateAuthenticationRequest request)
        {
            ServiceLocator.CommandBus.Send(new EditAuthenticationCommand(Guid.NewGuid(), request.AuthType, request.AccountId
                , request.Account, request.Status));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static VerifyCodeResponse GetValidVerifyCodeByAccount(GetVerifyCodeRequest request)
        {
            var verifyCode = ServiceLocator.AuthenticationReportDatabase.GetVerifyRegisterCode(request.AuthType, request.Account, request.AccountId);
            var result = new VerifyCodeResponse();
            result.Status = 10001;
            if (!string.IsNullOrEmpty(verifyCode))
            {
                result.VerifyCode = verifyCode;
            }
            return result;
        }

        public static AssociatorResponse AddAssociator(AddAssociatorRequest request)
        {
            var associatorId = Guid.NewGuid();
            ServiceLocator.CommandBus.Send(new CreatAssociatorCommand(associatorId, request.KindId, request.AccountId
                , request.PayOrderCode, request.PayType, request.Amount, request.Quantity));
            var result = new AssociatorResponse();
            result.Status = 10001;
            result.Associator = new Associator();
            result.Associator.AssociatorId = associatorId.ToString();
            return result;
        }
        public static AccountResultResponse UpdateAssociatorStatus(UpdateAssociatorStatusRequest request)
        {
            ServiceLocator.CommandBus.Send(new EditAssociatorCommand(new Guid(request.AssociatorId), request.Status));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static SysKindListResponse GetSysKindList(int kind)
        {
            var list = ServiceLocator.KindReportDatabase.GetSysKind(kind);
            var result = new SysKindListResponse();
            result.Status = 10001;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var sysKind = new SysKind();
                    sysKind.KindId = item.Id.ToString();
                    sysKind.Price = item.Price;
                    sysKind.Quantity = item.Quantity;
                    sysKind.Unit = item.Unit;
                    sysKind.Description = item.Description;
                    sysKind.DiscountValue = item.DiscountValue;
                    result.KindList.Add(sysKind);
                }
            }
            return result;
        }
        public static AssociatorListResponse GetAssociatorByAccountId(string accountId)
        {
            var list = ServiceLocator.AssociatorReportDatabase.GetAssociatorByAccountId(accountId);
            var result = new AssociatorListResponse();
            result.Status = 10001;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var associator = new Associator();
                    associator.AssociatorId = item.Id.ToString();
                    associator.AccountId = item.AccountId;
                    associator.Amount = item.Amount;
                    associator.EndDate = item.EndDate.Ticks;
                    associator.KindId = item.KindId;
                    associator.PayOrderCode = item.PayOrderCode;
                    associator.PayType = item.PayType;
                    associator.Quantity = item.Quantity;
                    associator.StartDate = item.StartDate.Ticks;
                    result.AssociatorList.Add(associator);
                }
            }
            return result;
        }
        public static AssociatorListResponse GetDiscountByAccountId(string accountId,int kind)
        {
            var list = ServiceLocator.AssociatorReportDatabase.GetDiscountByAccountId(accountId, kind);
            var result = new AssociatorListResponse();
            result.Status = 10001;
            if (list != null)
            {
                foreach (var item in list)
                {
                    var associator = new Associator();
                    associator.AssociatorId = item.Id.ToString();
                    associator.AccountId = item.AccountId != null ? item.AccountId:string.Empty;
                    associator.Amount = item.Amount;
                    associator.EndDate = item.EndDate.Ticks;
                    associator.KindId = item.KindId != null ? item.KindId : string.Empty;
                    associator.PayOrderCode = item.PayOrderCode != null ? item.PayOrderCode : string.Empty; 
                    associator.PayType = item.PayType;
                    associator.Quantity = item.Quantity;
                    associator.StartDate = item.StartDate.Ticks;
                    associator.DiscountValue = item.DiscountValue;
                    associator.Description = item.Description != null ? item.Description : string.Empty;
                    associator.Quantity = item.Quantity;
                    result.AssociatorList.Add(associator);
                }
            }
            return result;
        }

        public static AccountFullInfoResponse GetAccountFullInfo(string accountId)
        {
            var domain = ServiceLocator.AccountInfoReportDatabase.GetAccountInfoById(accountId);
            var result = new AccountFullInfoResponse();
            result.Status = 10001;
            if (domain != null)
            {
                result.Avatar = domain.Avatar!= null ? domain.Avatar:string.Empty;
                result.FullName = domain.Fullname!= null? domain.Fullname:string.Empty;
                result.Gender = domain.Gender == 1;
                result.PayPassWord = domain.PayPassWord != null ? domain.PayPassWord : string.Empty;
                result.UserType = domain.UserType;
                var date = ServiceLocator.AccountInfoReportDatabase.GetAssociatorDateByAId(accountId);
                if(date > DateTime.MinValue)
                {
                    result.AssociatorDate = date.Ticks;
                }
                else
                {
                    result.AssociatorDate = 0;
                }
            }
            return result;
        }
        public static AccountResultResponse UpdateAccountFullInfo(AccountFullInfoRequest request)
        {
            ServiceLocator.CommandBus.Send(new EditAccountInfoCommand(request.AccountId, request.FullName, request.Gender
                , request.Avatar, request.UserType, request.DormId));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AssociatorResponse GetAssociatorById(string associatorId)
        {
            var domain = ServiceLocator.AssociatorReportDatabase.GetAssociatorById(associatorId);
            var result = new AssociatorResponse();
            result.Status = 10001;
            if (domain != null)
            {
                result.Associator = new Associator();
                result.Associator.Amount = domain.Amount;
                result.Associator.AssociatorId = domain.Id.ToString();
                result.Associator.PayOrderCode = domain.PayOrderCode != null ? domain.PayOrderCode : string.Empty;
                result.Associator.EndDate = domain.EndDate != null ? domain.EndDate.Ticks:0;
                result.Associator.StartDate = domain.EndDate != null ? domain.StartDate.Ticks : 0;
                result.Associator.Quantity = domain.Quantity ;
                result.Associator.PayType = domain.PayType;
            }
            return result;
        }
        public static AccountResultResponse SetAccountPayPwd(string accountId, string payPwd)
        {
            ServiceLocator.CommandBus.Send(new CreateAccountPayPwdCommand(new Guid(accountId), payPwd));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }
        public static AccountResultResponse UpdateAccountPayPwd(string accountId, string payPwd)
        {
            ServiceLocator.CommandBus.Send(new EditAccountPayPwdCommand(new Guid(accountId), payPwd));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AccountResultResponse UpdateAccountLoginPwd(string accountId, string pwd)
        {
            ServiceLocator.CommandBus.Send(new EditAccountPwdCommand(new Guid(accountId), pwd));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }
        public static AccountResultResponse UpdateAccountMobile(string accountId, string mobilePhone)
        {
            ServiceLocator.CommandBus.Send(new EditAccountMobileCommand(new Guid(accountId), mobilePhone));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }
        public static AccountResultResponse BindOtherAccount(string accountId, int otherType, string otherAccount)
        {
            ServiceLocator.CommandBus.Send(new BindOtherAccountCommand(new Guid(accountId), otherAccount, (OtherType)otherType));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AccountResultResponse CreateOtherAccount(string mobilePhone, int otherType, string otherAccount, string fullName, string avatar, bool gender)
        {
            ServiceLocator.CommandBus.Send(new CreateOtherAccountCommand(mobilePhone, otherAccount, (OtherType)otherType, fullName,  avatar,  gender));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }

        public static AccountResponse GetOtherAccount(string otherAccount, OtherType otherType)
        {
            var account = ServiceLocator.ReportDatabase.GetOtherAccount(otherAccount, otherType);
            var result = new AccountResponse();
            result.Status = 10001;
            if (account != null)
            {
                result.MobilePhone = account.MobilePhone;
                result.Email = account.Email;
                result.Password = account.Password;
                result.AccountId = account.AccountId;
            }
            return result;
        }
        public static AccountResultResponse UpdateAccountIDInfo(string accountId, int dormId, string fullName, int userType)
        {
            ServiceLocator.CommandBus.Send(new CreateAccountIDCardCommand(new Guid(accountId), dormId, fullName, userType));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }
        public static AccountResultResponse ApplyPartner(string accountId,int dormId)
        {
            ServiceLocator.CommandBus.Send(new CreateApplyPartnerCommand(new Guid(accountId),dormId));
            var result = new AccountResultResponse();
            result.Status = 10001;
            return result;
        }
    }
}
