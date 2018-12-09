using SP.Api.Model;
using SP.Api.Model.Account;
using SP.Api.Model.Seller;
using SP.Service;
using System;
using System.Collections.Generic;
using static SP.Service.AccountService;

namespace AccountGRPCInterface
{
    public class AccountBusiness
    {
        public static AccountModel GetAccount(string account)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountRequest()
            {
                Account = account
            };
            var reuslt = client.GetAccount(request1);
            if (reuslt.Status == 10001)
            {
                var model = new AccountModel();
                model.AccountId = reuslt.AccountId;
                model.Email = reuslt.Email;
                model.MobilePhone = reuslt.MobilePhone;
                model.Password = reuslt.Password;
                model.AliBind = reuslt.AliBind;
                model.WxBind = reuslt.WxBind;
                model.QQBind = reuslt.QQBind;
                return model;
            }
            else
            {
                return null;
            }
        }

        public static AccountModel GetAccountDetail(string accountId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId
            };
            var reuslt = client.GetAccountDetail(request1);
            if (reuslt.Status == 10001)
            {
                var model = new AccountModel();
                model.AccountId = reuslt.AccountId;
                model.Email = reuslt.Email;
                model.MobilePhone = reuslt.MobilePhone;
                model.Password = reuslt.Password;
                model.AliBind = reuslt.AliBind;
                model.WxBind = reuslt.WxBind;
                model.QQBind = reuslt.QQBind;
                model.UserType = reuslt.UserType;
                return model;
            }
            else
            {
                return null;
            }
        }
        public  static string RegistAccount(AccountModel model)
        {
            string reuslt = string.Empty;
            var client = AccountClientHelper.GetClient();
            var request1 = new RegistRequest()
            {
                Email = model.Email,
                MobilePhone = model.MobilePhone,
                PassWord = model.Password,
                UserName = model.UserName
            };
            var result = client.RegistAccount(request1);
            if(result.Status == 10001)
            {
                var account = client.GetAccount(new AccountRequest()
                {
                    Account = string.IsNullOrEmpty(model.Email) ? model.MobilePhone : model.Email
                });
                reuslt = account.AccountId;
            }
            return reuslt;
        }
        public static string UpdateAccount(AccountModel model)
        {
            string reuslt = string.Empty;
            var client = AccountClientHelper.GetClient();
            var request1 = new UpdateAccountRequest()
            {
                Email = model.Email,
                MobilePhone = model.MobilePhone,
                PassWord = model.Password,
                AccountId = model.AccountId
            };
            var result = client.UpdateAccount(request1);
            if (result.Status == 10001)
            {
                reuslt = model.AccountId;
            }
            return reuslt;
        }

        public static TokenModel GetAccessToken(string userKey)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new TokenKeyRequest()
            {
                Key = userKey
            };
            var result = client.GetAccessToken(request1);
            var domain = new TokenModel();
            if (result.Status == 10001)
            {
                domain.AccountId = result.AccessToken.AccountId;
                domain.Access_Token = result.AccessToken.AccessToken_;
                domain.Access_Token_Expires = new DateTime(result.AccessToken.AccessTokenExpires).ToString("yyyy-MM-dd HH:mm:ss");
                domain.Refresh_Token = result.AccessToken.RefreshToken;
                domain.Refresh_Token_Expires = new DateTime(result.AccessToken.RefreshTokenExpires).ToString("yyyy-MM-dd HH:mm:ss");
            }

            return domain;
        }

        public static TokenModel GetAccessTokenByRefreshToken(string refreshToken)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new TokenKeyRequest()
            {
                Key = refreshToken
            };
            var result = client.GetAccessTokenByRefreshToken(request1);
            var domain = new TokenModel();
            if (result.Status == 10001)
            {
                domain.AccountId = result.AccessToken.AccountId;
                domain.Access_Token = result.AccessToken.AccessToken_;
                domain.Access_Token_Expires = new DateTime(result.AccessToken.AccessTokenExpires).ToString("yyyy-MM-dd HH:mm:ss");
                domain.Refresh_Token = result.AccessToken.RefreshToken;
                domain.Refresh_Token_Expires = new DateTime(result.AccessToken.RefreshTokenExpires).ToString("yyyy-MM-dd HH:mm:ss");
            }

            return domain;
        }

        public static bool AddAccessToken(OAuth2AccessToken token)
        {
            bool status = false;
            var client = AccountClientHelper.GetClient();
            var request = new AccessTokenRequest()
            {
                 AccessToken = new AccessToken()
                 {
                     AccountId = token.AccountID,
                     AccessToken_ = token.AccessToken,
                     RefreshToken = token.RefreshToken,
                     AccessTokenExpires = token.AccessTokenExpires.Value.Ticks,
                     RefreshTokenExpires = token.RefreshTokenExpires.Value.Ticks
                 }
            };
            var result = client.AddAccessToken(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }

        public static bool RemoveAccessToken(string token,string accountId)
        {
            bool status = false;
            var client = AccountClientHelper.GetClient();
            var request = new TokenIdRequest()
            {
                AccessToken = token,
                AccountId = accountId,
            };
            var result = client.RemoveAccessToken(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }

        public static bool AddAccountAuthentication(AuthenticationModel model)
        {
            bool status = false;
            var client = AccountClientHelper.GetClient();
            var request = new AddAuthenticationRequest()
            {
                AccountAuth = new AccountAuthentication()
                {
                    AccountId = model.AccountId,
                    Account = model.Account,
                    AuthType = (int)model.AuthType,
                    Token = model.Token,
                    VerifyCode = model.VerifyCode
                }
            };
            var result = client.AddAccountAuthentication(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }

        public static bool UpdateAuthStatusByAccount(AuthenticationModel model)
        {
            bool status = false;
            var client = AccountClientHelper.GetClient();
            var request = new UpdateAuthenticationRequest()
            {
                Account = model.Account,
                AccountId = model.AccountId,
                AuthType = (int)model.AuthType,
                Status = (int)model.Status
            };
            var result = client.UpdateAuthStatusByAccount(request);
            if (result.Status == 10001)
            {
                status = true;
            }
            return status;
        }

        public static string GetValidVerifyCodeByAccount(AuthenticationModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new GetVerifyCodeRequest()
            {
                Account = model.Account,
                AccountId = model.AccountId,
                AuthType = (int)model.AuthType,                
            };
            var result = client.GetValidVerifyCodeByAccount(request);
            if (result.Status == 10001)
            {
                return result.VerifyCode;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string AddAssociator(AssociatorModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new AddAssociatorRequest()
            {
                KindId = model.kindId,
                AccountId = model.accountId,
                //PayOrderCode = model.payOrderCode,
                PayType = model.payType,
                //Amount = model.amount,
                Quantity = model.quantity,
            };
            var result = client.AddAssociator(request);
            if (result.Status == 10001)
            {
                return result.Associator.AssociatorId;
            }
            else
            {
                return string.Empty;
            }
        }
        public static bool UpdateAssociatorStatus(AssociatorModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new UpdateAssociatorStatusRequest()
            {
                 AssociatorId = model.associatorId,
                 Status = model.status
            };
            var result = client.UpdateAssociatorStatus(request);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static AssociatorModel GetAssociatorById(string associatorId)
        {
            var client = AccountClientHelper.GetClient();
            var request = new AssociatorIdRequest()
            {
                AssociatorId = associatorId
            };
            var result = client.GetAssociatorById(request);
            var domain = new AssociatorModel();
            if (result.Status == 10001)
            {
                domain.amount = result.Associator.Amount;
                domain.associatorId = result.Associator.AssociatorId;
                domain.endDate = new DateTime(result.Associator.EndDate);
                domain.startDate = new DateTime(result.Associator.StartDate);
                domain.payOrderCode = result.Associator.PayOrderCode;
                domain.quantity = result.Associator.Quantity;
                domain.payType = result.Associator.PayType;
            }
            return domain;
        }
        public static AssociatorModel GetAssociatorByCode(string associatorCode)
        {
            var client = AccountClientHelper.GetClient();
            var request = new AssociatorCodeRequest()
            {
                AssociatorCode = associatorCode
            };
            var result = client.GetAssociatorByCode(request);
            var domain = new AssociatorModel();
            if (result.Status == 10001)
            {
                domain.amount = result.Associator.Amount;
                domain.associatorId = result.Associator.AssociatorId;
                domain.endDate = new DateTime(result.Associator.EndDate);
                domain.startDate = new DateTime(result.Associator.StartDate);
                domain.payOrderCode = result.Associator.PayOrderCode;
                domain.quantity = result.Associator.Quantity;
                domain.payType = result.Associator.PayType;
            }
            return domain;
        }

        public static List<SysKindModel> GetAssociatorKindList()
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new GetSysKindRequest()
            {
                Kind = 100,
            };
            var result = client.GetSysKindList(request1);
            var list = new List<SysKindModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.KindList)
                {
                    var domain = new SysKindModel();
                    domain.KindId = item.KindId;
                    domain.Price = item.Price;
                    domain.Quantity = item.Quantity;
                    domain.Unit = item.Unit;
                    domain.Description = item.Description;
                    domain.DiscountValue = item.DiscountValue;
                    list.Add(domain);
                }
            }
            return list;
        }

        public static List<SysKindModel> GetCouponsKindList()
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new GetSysKindRequest()
            {
                Kind = 200,
            };
            var result = client.GetSysKindList(request1);
            var list = new List<SysKindModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.KindList)
                {
                    var domain = new SysKindModel();
                    domain.KindId = item.KindId;
                    domain.Price = item.Price;
                    domain.Quantity = item.Quantity;
                    domain.Unit = item.Unit;
                    domain.Description = item.Description;
                    domain.DiscountValue = item.DiscountValue;
                    list.Add(domain);
                }
            }
            return list;
        }
        public static AccountFullInfoModel GetAccountFullInfo(string accountId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIdRequest()
            {
                AccountId = accountId
            };
            var reuslt = client.GetAccountFullInfo(request1);
            if (reuslt.Status == 10001)
            {
                var model = new AccountFullInfoModel();
                model.Avatar = reuslt.Avatar;
                model.FullName = reuslt.FullName;
                model.Gender = reuslt.Gender;
                model.PayPassWord = reuslt.PayPassWord;
                model.UserType = reuslt.UserType;
                if(reuslt.AssociatorDate > 0)
                {
                    var currrentDate = new DateTime(reuslt.AssociatorDate);                    
                    model.AssociatorDate = GetTimestamp(currrentDate);
                    if(currrentDate > DateTime.Now)
                    {
                        model.IsAssociator = true;
                    }
                    else
                    {
                        model.IsAssociator = false;
                    }
                }
                return model;
            }
            else
            {
                return null;
            }
        }
        public static bool UpdateAccountFullInfo(AccountFullInfoModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new AccountFullInfoRequest()
            {
                AccountId = model.AccountId,
                Avatar = string.IsNullOrEmpty(model.Avatar)?string.Empty: model.Avatar,
                FullName = model.FullName,
                Gender = model.Gender,
                DormId = model.DormId
            };
            var result = client.UpdateAccountFullInfo(request);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static List<DiscountModel> GetDiscountByAccountId(string accountId,int kind)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new DiscountRequest()
            {
                AccountId = accountId,
                Kind = kind,
            };
            var result = client.GetDiscountByAccountId(request1);
            var list = new List<DiscountModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.AssociatorList)
                {
                    var domain = new DiscountModel();
                    domain.Amount = item.Amount;
                    domain.EndDate = new DateTime(item.EndDate).ToString("yyyy-MM-dd");
                    domain.Quantity = item.Quantity;
                    domain.StartDate = new DateTime(item.StartDate).ToString("yyyy-MM-dd");
                    domain.Description = item.Description;
                    domain.DiscountValue = item.DiscountValue;                    
                    list.Add(domain);
                }
            }
            return list;
        }
        public static bool SetAccountPayPwd(PayPwdModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new AccountPayPwdRequest()
            {
                AccountId = model.AccountId,
                PayPwd = model.Password
            };
            var result = client.SetAccountPayPwd(request);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdateAccountPayPwd(PayPwdModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new AccountPayPwdRequest()
            {
                AccountId = model.AccountId,
                PayPwd = model.Password
            };
            var result = client.UpdateAccountPayPwd(request);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool UpdateAccountLoginPwd(PayPwdModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new AccountPayPwdRequest()
            {
                AccountId = model.AccountId,
                PayPwd = model.Password
            };
            var result = client.UpdateAccountLoginPwd(request);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool UpdateAccountMobile(AccountModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new AccountMobileRequest()
            {
                AccountId = model.AccountId,
                MobilePhone = model.MobilePhone
            };
            var result = client.UpdateAccountMobile(request);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool BindOtherAccount(BingAccountModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new BingAccountRequest()
            {
                AccountId = model.AccountId,
                OtherAccount = model.OtherAccount,
                OtherType = model.OtherType,
            };
            var result = client.BindOtherAccount(request);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool CreateOtherAccount(OtherAccountModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request = new OtherAccountRequest()
            {
                MobilePhone = model.MobilePhone,
                OtherAccount = model.OtherAccount,
                OtherType = model.OtherType,
                Avatar = model.Avatar,
                FullName = model.FullName,
                Gender = model.Gender
            };
            var result = client.CreateOtherAccount(request);
            if (result.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static AccountModel GetOtherAccount(string otherAccount,int otherType)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new GetOtherAccountRequest()
            {
                OtherAccount = otherAccount,
                OtherType = otherType
            };
            var reuslt = client.GetOtherAccount(request1);
            if (reuslt.Status == 10001)
            {
                var model = new AccountModel();
                model.AccountId = reuslt.AccountId;
                model.Email = reuslt.Email;
                model.MobilePhone = reuslt.MobilePhone;
                model.Password = reuslt.Password;
                return model;
            }
            else
            {
                return null;
            }
        }
        public static bool UpdateAccountIDInfo(AccountIDModel model)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIDRequest()
            {
                AccountId = model.AccountId,
                DormId = model.DormId,
                UserType = model.UserType,
                FullName = model.FullName
            };
            var reuslt = client.UpdateAccountIDInfo(request1);
            if (reuslt.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool ApplyPartner(string accountId,int dormId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new ApplyPartnerRequest()
            {
                AccountId = accountId,
                DormId = dormId
            };
            var reuslt = client.ApplyPartner(request1);
            if (reuslt.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool BalancePay(string accountId, string token, string password, double amount, string orderCode,string sign)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new BalancePayRequest()
            {
                AccountId = accountId,
                Amount = amount,
                OrderCode = orderCode,
                PassWord = password,
                Token = token,
                Sign = sign
            };
            var reuslt = client.BalancePay(request1);
            if (reuslt.Status == 10001)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<TradeInfoModel> GetTradeList(string accountId, int pageIndex, int pageSize)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new TradeListRequest()
            {
                AccountId = accountId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var reuslt = client.GetTradeList(request1);
            var list = new List<TradeInfoModel>();
            if (reuslt.Status == 10001)
            {
                foreach (var item in reuslt.TradeList)
                {
                    var model = new TradeInfoModel();
                    model.Type = item.Type;
                    model.TradeNo = item.TradeNo;
                    model.BalanceAmount = item.BalanceAmount;
                    var date = new DateTime(item.CreateTime);
                    model.CeateTime = GetTimestamp(date);
                    model.Amount = item.Amount;
                    list.Add(model);
                }
            }
            return list;
        }
        public static long GetTradeListCount(string accountId)
        {
            var client = AccountClientHelper.GetClient();
            var request1 = new AccountIDRequest()
            {
                AccountId = accountId
            };
            var reuslt = client.GetTradeListCount(request1);
            long total = 0;
            if (reuslt.Status == 10001)
            {
                total = reuslt.Total;
            }
            return total;
        }

        private static long GetTimestamp(DateTime d)
        {
            return (d.ToUniversalTime().Ticks - 621355968000000000) / 10000000;     //精确到毫秒
        }
    }
}
