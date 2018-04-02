﻿using AccountGRPCInterface;
using SmsGRPCInterface;
using SP.Api.Cache;
using SP.Api.Model.Account;
using SP.Api.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiGateway.App_Start;
using WebApiGateway.App_Start.Crypt;
using WebApiGateway.App_Start.Enum;
using WebApiGateway.Models.Regist;
using WebApiGateway.Models.Token;

namespace WebApiGateway.Controllers.Register
{
    public class RegisterController : ApiController
    {
        public ApiEnum.ErrorCode errorCode = ApiEnum.ErrorCode.Success;
        public string errorMsg = string.Empty;

        #region 注册

        /// <summary>
        /// 发送注册手机验证码
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel Send_Register_Code([FromBody] RegisterAccount Params)
        {
            Params.account = Common.CheckAccount(Params.account);

            if (string.IsNullOrEmpty(Params.account))
            {
                return new ResultModel() { code = ApiEnum.ErrorCode.ComBad, error_msg = "发送失败" };
            }

            //账号已经存在
            var userAccount = AccountBusiness.GetAccount(Params.account);
            if (userAccount != null && !string.IsNullOrEmpty(userAccount.AccountId) && !Params.is_authorization)
            {
                errorCode = ApiEnum.ErrorCode.AlreadyAccount;
            }
            else
            {
                bool isSendLimit = false;
                if (!SendRegisterVerifyCode((VerifyCodeType)Params.type, Params.is_authorization, Params.account, out isSendLimit))
                {
                    if (isSendLimit)
                    {
                        errorCode = ApiEnum.ErrorCode.ComBad;
                        errorMsg = "发送短信过于频繁";
                    }
                    else
                    {
                        errorCode = ApiEnum.ErrorCode.ComBad;
                        errorMsg = "发送失败";
                    }
                }
            }

            return new ResultModel() { code = errorCode, error_msg = errorMsg };
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="Params"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel Register_Account([FromBody] RegisterAccount Params)
        {
            Params.account = Common.CheckAccount(Params.account);

            if (string.IsNullOrEmpty(Params.account))
            {
                return new ResultModel() { code = ApiEnum.ErrorCode.ComBad, error_msg = "注册失败" };
            }

            if (!string.IsNullOrEmpty(Params.code))
            {
                if (!VerifyRegisterCode(Params.code, Params.account))
                    return new ResultModel() { code = ApiEnum.ErrorCode.ComBad, error_msg = "验证码错误" };
            }
            else
                return new ResultModel() { code = ApiEnum.ErrorCode.ComBad, error_msg = "验证码不能为空" };

            if (!string.IsNullOrEmpty(Params.password))
            {
                if (!Common.ValidatePassword(Params.password))
                {
                    errorCode = ApiEnum.ErrorCode.ComBad;
                    errorMsg = "密码不符合规范";
                }
                bool isEmail = Common.ValidateEmail(Params.account);
                //var accountId = MD.Business.Account.InsertAccount(Params.account, Params.password, Params.full_name, string.Empty, string.Empty);
                var pwd = StringCrypt.Encrypt(Params.password, ConfigInfo.ConfigInfoData.CryptKey.MessageKey);
                var model = new AccountModel()
                {
                    Email = isEmail ? Params.account : string.Empty,
                    MobilePhone = !isEmail ? Params.account : string.Empty,
                    Password = pwd,
                    UserName = Params.full_name
                };
                var accountId = AccountBusiness.RegistAccount(model);
                if (string.IsNullOrEmpty(accountId))
                {
                    errorCode = ApiEnum.ErrorCode.ComBad;
                    errorMsg = "注册失败";
                }
                else
                {
                    var accessTokenModel = new TokenModel();

                    TokenCache.Add(accessTokenModel, accountId);
                    
                    return new ResultModel() { code = errorCode, error_msg = errorMsg,access_token= accessTokenModel?.Access_Token??string.Empty };
                }
            }

            return new ResultModel() { code = errorCode, error_msg = errorMsg };
        }
        #endregion

        #region Help
        private bool SendRegisterVerifyCode(VerifyCodeType verifyCodeType, bool isFristTime, string account, out bool isSendLimit)
        {
            bool isEmail = Common.ValidateEmail(account);

            var authType = isEmail ? AuthType.Register : AuthType.MobilePhoneRegister;

            isSendLimit = false;

            string code = Common.GetRandomNumberStr(4);
            bool sendCodeSuccess = false;
            var reuslt = AccountBusiness.AddAccountAuthentication(new AuthenticationModel()
            {
                 Account = account,
                 AuthType = AuthType.MobilePhoneRegister,
                 VerifyCode = code,  
                 AccountId=string.Empty,
                 Token = string.Empty
            });
            
            if (reuslt)
            {
                if(!isEmail)
                {
                    

                    if (!CheckIsAllowSendRegisterMobileMessage(account))
                    {
                        isSendLimit = true;
                        sendCodeSuccess = false;
                    }
                    else
                    {
                        if (verifyCodeType ==VerifyCodeType.SMS) //短信验证码
                        {
                            sendCodeSuccess = SendRegisterSMSCode(code, account);
                        }
                        else if (verifyCodeType == VerifyCodeType.Voice) //语音验证码
                        {
                            //sendCodeSuccess = MD.Sms.SmsGrpc.SendVoiceMessage(account, code);
                        }

                        SetRegisterMobileMessageLimit(account);
                    }
                }
            }

            return sendCodeSuccess;
        }

        private bool SendRegisterSMSCode(string code, string account)
        {
            if(account.StartsWith("86"))
            {
                account = account.Substring(2);
            }
            if (account.StartsWith("+86"))
            {
                account = account.Substring(3);
            }
            var model = SmsBusiness.SendMessage(account, "尊敬的用户，您的注册验证码为：" + code + "，请勿泄漏于他人！", "");
            if(model.Code == 1)
            {
                return true;
            }
            return false;
        }

        private bool VerifyRegisterCode(string code, string account)
        {
            var authType = Common.ValidateEmail(account) ? AuthType.Register : AuthType.MobilePhoneRegister;

            string verifyCode = AccountBusiness.GetValidVerifyCodeByAccount(new AuthenticationModel()
            {
                Account = account,
                VerifyCode = code,
                AuthType = AuthType.MobilePhoneRegister,
                AccountId = string.Empty,
                Token = string.Empty
            });

            if (!string.IsNullOrEmpty(verifyCode) && code.Equals(verifyCode))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注册
        /// </summary>
        private bool CheckIsAllowSendRegisterMobileMessage(string mobilePhone)
        {
            mobilePhone = mobilePhone.Replace("+", string.Empty);
            
            bool isAllow = SmsBusiness.CheckIsAllowSendRegisterMobileMessage(mobilePhone, Common.GetRealIPAddress());
            return isAllow;
        }

        /// <summary>
        /// 注册发送数量设置
        /// </summary>
        private void SetRegisterMobileMessageLimit(string mobilePhone)
        {
            mobilePhone = mobilePhone.Replace("+", string.Empty);
            
            SmsBusiness.SetRegisterMobileMessageLimit(mobilePhone, Common.GetRealIPAddress());
        }


        #endregion
    }
}
