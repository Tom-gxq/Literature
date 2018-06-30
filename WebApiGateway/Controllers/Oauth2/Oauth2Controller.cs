using AccountGRPCInterface;
using SP.Api.Cache;
using SP.Api.Model.Account;
using SP.Api.Model.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using WebApiGateway.App_Start;
using WebApiGateway.App_Start.Crypt;
using WebApiGateway.App_Start.Enum;
using WebApiGateway.App_Start.Result;
using WebApiGateway.App_Start.RSA;
using WebApiGateway.Controllers.Base;
using WebApiGateway.Models.Oauth2;
using WebApiGateway.Models.Token;

namespace WebApiGateway.Controllers.Oauth2
{
    public class Oauth2Controller : OauthBaseController
    {        
        #region 获取token
        [HttpPost]
        public ActionResult Access_token(Oauth2TokenForm Oauth2TokenForm)
        {
            try
            {
                grantType = Oauth2TokenForm.grant_type;

                if (string.IsNullOrEmpty(grantType))
                {
                    error_code = ApiEnum.ErrorCode.ComLoseParam;
                    error_msg = "缺少参数 grantType";
                }
                else
                {
                    if (grantType == "authorization_code")
                    {
                        //短信登陆
                        GetTokenByCode();
                    }
                    else if (grantType == "password")
                    {
                        //提供给移动端个人化登录
                        if (!string.IsNullOrEmpty(Request["account"]))
                        {
                            format = "json";
                            GetAccountTokenByPwd();
                        }
                    }
                    else if (grantType == "refresh_token")
                    {
                        GetTokenByRefreshToken();
                    }
                    else if (grantType == "other_account")
                    {
                        //第三方登陆
                        GetTokenByOther();
                    }
                    else
                    {
                        error_code = ApiEnum.ErrorCode.ComBadParam;
                        error_msg = "grantType 类型不存在";
                    }
                }
                if (error_code != ApiEnum.ErrorCode.Success)
                {
                    return Json(new ErrorModel()
                    {
                        error_code = (int)error_code,
                        error_msg = error_msg,
                        success = false
                    }, JsonRequestBehavior.AllowGet);
                }

                if (accessTokenModel != null && !string.IsNullOrEmpty(accessTokenModel.Access_Token))
                {
                    //增加登录日志
                   

                    //微信关注后登录提醒
                   

                    accessTokenModel.Success = true;
                    

                    return new XmlResult(accessTokenModel, accessTokenModel.GetType(), format);
                }
                else
                    return Json(new { }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error_msg = error_msg + ex.Message +"\n"+ex.StackTrace }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 是否授权
        public ActionResult IsAuthorize(AuthorizeAjaxForm Params)
        {
            var flag = IsAuthorization(Params.accountId, false);

            return Json(new { result = flag }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取token的几种形式  
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
        [System.Web.Mvc.NonAction]
        public void GetTokenByCode()
        {
            string authorization_code = Request["code"];

            if (string.IsNullOrEmpty(authorization_code) )
            {
                error_code = ApiEnum.ErrorCode.ComLoseParam;
                error_msg = "authorization_code：缺少参数code或者redirect_uri";
            }
            else
            {
                //var codeParam = Oauth2Help.GetAppAccountCode(authorization_code);
                //if (codeParam != null)
                var account =  Request["account"];
                if (!string.IsNullOrEmpty(account) && !account.StartsWith("+86") && !account.StartsWith("86"))
                {
                    if (account.StartsWith("86"))
                    {
                        account = "+" + account;
                    }
                    else
                    {
                        account = "+86" + account;
                    }
                }
                if (VerifyRegisterCode(authorization_code, account))                
                {
                    var accountEntity = AccountBusiness.GetAccount(account);
                    if (accountEntity == null)
                    {
                        accountResult = AccountResult.AccountError;
                    }
                    else
                    {
                        accountId = accountEntity.AccountId;
                        error_msg += accountId + "#@  ";
                        try
                        {
                            MDSession.Session.Clear();
                            MDSession.Session["Account"] = AccountInfoCache.GetAccountInfoByAccountId(accountId);
                            accessTokenModel = new TokenModel();
                            TokenCache.Add(accessTokenModel, accountId);
                        }
                        catch(Exception ex)
                        {
                        }
                    }
                    
                }
                else
                {
                    error_code = ApiEnum.ErrorCode.CodeLose;
                    error_msg = "authorization_code：code已经失效";
                }
            }
        }

        [System.Web.Mvc.NonAction]
        public void GetAccountTokenByPwd()
        {
            string username = Request["account"];
            string password = Request["password"];

            try
            {
                username = RSAHelp.RSADecrypt(string.Empty, username);
                password = RSAHelp.RSADecrypt(string.Empty, password);
            }
            catch (Exception ex)
            {
                username = Request["account"];
                password = Request["password"];
            }

            if (!string.IsNullOrEmpty(username) && !Common.ValidateEmail(username) && !username.Contains("+"))
                username = "+86" + username;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                error_code = ApiEnum.ErrorCode.ComLoseParam;
                error_msg = "password：缺少参数username或者password";
            }
            else
            {
                var loginErrorCache = LoginErrorCache.Get(username.ToLower());

                if (loginErrorCache !=null && Convert.ToInt32(loginErrorCache) == 10)
                {
                    error_code = ApiEnum.ErrorCode.MoreLogin;
                    error_msg = "password：由于您的帐号尝试多次登录失败，已被锁定，请20分钟后再试";
                }
                else
                {
                    ValidateLogin(username, password);

                    if (accountResult == AccountResult.AccountSuccess)
                    {
                        LoginErrorCache.Delete(username.ToLower());

                        accessTokenModel = new TokenModel();

                        TokenCache.Add(accessTokenModel, accountId);
                        
                    }
                    else
                    {
                        //用户名或密码错误
                        if (accountResult == AccountResult.AccountError || accountResult == AccountResult.PasswordError)
                        {
                            int errorCount = 1;
                            if (loginErrorCache != null)
                            {
                                errorCount = Convert.ToInt32(loginErrorCache) + 1;
                                if (errorCount == 10)
                                    error_msg += "，您的帐号已被锁定，请在20分钟以后尝试登录";
                                else
                                    error_msg += "，您还有" + (10 - errorCount) + "次尝试输入机会";
                            }
                            else
                                error_msg += "，您还有9次尝试输入机会";
                            LoginErrorCache.Set(username.ToLower(), errorCount, DateTime.Now.AddMinutes(20));

                            error_code = ApiEnum.ErrorCode.BadPasswordOrAccount;
                            error_msg = "账号或密码错误";
                        }
                        else
                        {
                            error_code = ApiEnum.ErrorCode.BadAccount;
                            error_msg = "用户状态不对 如用户未审核、拒绝申请、被屏蔽登录、已删除、被举报离职";
                        }
                    }
                }
            }
        }

        [System.Web.Mvc.NonAction]
        public void GetTokenByRefreshToken()
        {
            string refresh_token = Request["refresh_token"];

            if (string.IsNullOrEmpty(refresh_token))
            {
                error_code = ApiEnum.ErrorCode.ComLoseParam;
                error_msg = "refresh_token不能为空";
            }
            else
            {
                DateTime expires = DateTime.MinValue;
                var tokenModel = TokenCache.GetTokenByRefreshToken(refresh_token);
                
                if (tokenModel != null)
                {
                    if (!string.IsNullOrEmpty(tokenModel.Refresh_Token_Expires))
                    {
                        expires = DateTime.Parse(tokenModel.Refresh_Token_Expires);
                    }
                    if (expires >= DateTime.Now)
                    {
                        accountId = tokenModel.AccountId;

                        accessTokenModel = new TokenModel();
                        TokenCache.Add(accessTokenModel, accountId);

                        var account = AccountInfoCache.GetAccountInfoByAccountId(accountId);

                        try
                        {
                            MDSession.Session.Clear();
                            MDSession.Session["Account"] = account;
                        }
                        catch(Exception ex)
                        {
                        }
                    }
                    else
                    {
                        error_code = ApiEnum.ErrorCode.BaseDisabledToken;
                        error_msg = "refresh_token失效";
                    }
                }
                else
                {
                    error_code = ApiEnum.ErrorCode.BaseBadToken;
                    error_msg = "refresh_token对应的access_token不存在";
                }
                
            }
        }
        [System.Web.Mvc.NonAction]
        public void GetTokenByOther()
        {
            string account = Request["other_account"];
            string otherType = Request["other_type"];

            if (string.IsNullOrEmpty(account))
            {
                error_code = ApiEnum.ErrorCode.ComLoseParam;
                error_msg = "other_account：缺少参数";
            }
            else if(!string.IsNullOrEmpty(otherType))
            {
                 
                var accountEntity = AccountBusiness.GetOtherAccount(account,int.Parse(otherType));
                if (accountEntity == null||string.IsNullOrEmpty(accountEntity.AccountId))
                {
                    error_code = ApiEnum.ErrorCode.NotFound;
                }
                else
                {
                    accountId = accountEntity.AccountId;
                    error_msg += accountId + "#@  ";
                    try
                    {
                        MDSession.Session.Clear();
                        MDSession.Session["Account"] = AccountInfoCache.GetAccountInfoByAccountId(accountId);
                        accessTokenModel = new TokenModel();
                        TokenCache.Add(accessTokenModel, accountId);
                    }catch(Exception ex)
                    {
                    }
                }
            }
        }
        #endregion

        #region redis是否授权操作
        /// <summary>
        /// redis 该用户是否授权该应用
        /// </summary>
        [System.Web.Mvc.NonAction]
        public static bool IsAuthorization(string accountid, bool isGet)
        {
            string key = accountid.ToLower();

            var flag = false;

            if (isGet)
                flag = ApiTokenCache.GetIsAuthorization(key, appId);
            else
                flag = ApiTokenCache.SetIsAuthorization(key, appId,true);

            return flag;
        }
        #endregion

        #region 验证登录
        [System.Web.Mvc.NonAction]
        private void ValidateLogin(string account, string password)
        {
            //必须字段
            if (!string.IsNullOrEmpty(account))
            {
                account = Common.CheckAccount(account);
                //加密密码
                password = StringCrypt.Encrypt(password, ConfigInfo.ConfigInfoData.CryptKey.MessageKey);
                var accountEntity = AccountBusiness.GetAccount(account);
                if (accountEntity == null)
                {
                    accountResult = AccountResult.AccountError;
                }
                else
                {
                    if (accountEntity.Password != password)
                    {
                        accountResult = AccountResult.PasswordError; //密码错误
                    }
                    else
                    {
                        accountResult = AccountResult.AccountSuccess;
                        accountId = accountEntity.AccountId;

                        MDSession.Session.Clear();
                        MDSession.Session["Account"] = AccountInfoCache.GetAccountInfoByAccountId(accountEntity.AccountId);
                    }
                }
            }
        }
        #endregion
        

        public class AuthorizeAjaxForm
        {
            public string account { get; set; }
            public string psw { get; set; }
            public string projectId { get; set; }
            public string app_key { get; set; }
            public string appId { get; set; }
            public string redirect_uri { get; set; }
            public string state { get; set; }
            public string chkCode { get; set; }
            public string accountId { get; set; }
        }

        public class Oauth2TokenForm
        {
            public string app_key { get; set; }
            public string client_id { get; set; }
            public string app_secret { get; set; }
            public string client_secret { get; set; }
            public string grant_type { get; set; }
            public string p_signature { get; set; }
        }
        

        #region 验证码图片
        public ActionResult VerifyCode()
        {
            string checkCode = GenerateCheckCode();
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return Json(new { error_msg = "验证码生成失败" }, JsonRequestBehavior.AllowGet);

            Bitmap image = new Bitmap(100, 37);
            Graphics g = Graphics.FromImage(image);
            try
            {
                g.Clear(System.Drawing.Color.WhiteSmoke);

                Font font = new Font("Arial", 18, FontStyle.Regular);
                g.DrawString(checkCode, font, new SolidBrush(Color.Gray), 15, 5);

                g.DrawRectangle(new Pen(Color.WhiteSmoke), 0, 0, image.Width, image.Height);

                MemoryStream ms = new MemoryStream();
                image.Save(ms, ImageFormat.Gif);

                return new ImageResult(ms);
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        [System.Web.Mvc.NonAction]
        private string GenerateCheckCode()
        {
            string codeStr = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            string checkCode = string.Empty;
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int number = random.Next(0, codeStr.Length);
                checkCode += codeStr.Substring(number, 1);
            }
            if (string.IsNullOrEmpty(accountId))
                Session["mdVerifyCode"] = checkCode;
            else
                ApiTokenCache.SetAccountVerifyCode(accountId, checkCode.ToLower());

            return checkCode;
        }
        #endregion

        #region 第3方登录

        #region 微信

        //public ActionResult WeiXin_Login(TPLoginForm Params)
        //{
        //    if (!string.IsNullOrEmpty(Params.unionid) && !string.IsNullOrEmpty(Params.app_key) && !string.IsNullOrEmpty(Params.app_secret))
        //    {
        //        var tpAuth = MD.InterfaceService.TPAuth.CheckAccountByWeiXinParams(Params.unionid, Params.openid, Params.nickname, Params.headimgurl);

        //        if (tpAuth != null)
        //        {
        //            var accountId = tpAuth.AccountId;

        //            if (!string.IsNullOrEmpty(accountId))
        //            {
        //                return GetAccountLoginInfo(Params.app_key, accountId);
        //            }
        //            else
        //            {
        //                return Json(new
        //                {
        //                    error_code = 10008,
        //                    error_msg = "您的微信还没有绑定明道",
        //                    state = tpAuth.State,
        //                    unionId = Params.unionid,
        //                    unionid = Params.unionid,
        //                    success = false
        //                }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new
        //            {
        //                error_code = 10008,
        //                error_msg = "请求第3方登录服务失败",
        //                success = false
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            error_code = 10008,
        //            error_msg = "缺少参数",
        //            success = false
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult WeiXin_Bind(TPLoginForm Params)
        //{
        //    if (!string.IsNullOrEmpty(accountId) && !string.IsNullOrEmpty(Params.unionid) && !string.IsNullOrEmpty(Params.state))
        //    {
        //        MD.InterfaceService.TPAuth.BindAccount(Params.unionid, accountId, Params.state, MD.Enum.TPType.Weixin);
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            error_code = 10008,
        //            error_msg = "缺少参数",
        //            success = false
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        success = true
        //    }, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        #region 小米

        //public ActionResult XiaoMi_Login(TPLoginForm Params)
        //{
        //    if (!string.IsNullOrEmpty(Params.openid) && !string.IsNullOrEmpty(Params.app_key) && !string.IsNullOrEmpty(Params.app_secret))
        //    {
        //        var tpAuth = MD.InterfaceService.TPAuth.CheckAccountByXiaoMiParams(Params.openid, Params.nickname, Params.headimgurl);

        //        if (tpAuth != null)
        //        {
        //            var accountId = tpAuth.AccountId;

        //            if (!string.IsNullOrEmpty(accountId))
        //            {
        //                return GetAccountLoginInfo(Params.app_key, accountId);
        //            }
        //            else
        //            {
        //                return Json(new
        //                {
        //                    error_code = 10008,
        //                    error_msg = "您的小米还没有绑定明道",
        //                    state = tpAuth.State,
        //                    openid = Params.openid,
        //                    success = false
        //                }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        else
        //        {
        //            return Json(new
        //            {
        //                error_code = 10008,
        //                error_msg = "请求第3方登录服务失败",
        //                success = false
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            error_code = 10008,
        //            error_msg = "缺少参数",
        //            success = false
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        //public ActionResult XiaoMi_Bind(TPLoginForm Params)
        //{
        //    if (!string.IsNullOrEmpty(accountId) && !string.IsNullOrEmpty(Params.openid) && !string.IsNullOrEmpty(Params.state))
        //    {
        //        MD.InterfaceService.TPAuth.BindAccount(Params.openid, accountId, Params.state, MD.Enum.TPType.XiaoMi);
        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            error_code = 10008,
        //            error_msg = "缺少参数",
        //            success = false
        //        }, JsonRequestBehavior.AllowGet);
        //    }

        //    return Json(new
        //    {
        //        success = true
        //    }, JsonRequestBehavior.AllowGet);
        //}

        #endregion

        //private JsonResult GetAccountLoginInfo(string appKey, string accountId)
        //{
        //    var accountInfo = MD.Business.Account.GetAccountByAccountId(accountId);

        //    if (accountInfo != null)
        //    {
        //        if (appKey == "1672939984")
        //        {
        //            return Json(new
        //            {
        //                data = new
        //                {
        //                    username = !string.IsNullOrEmpty(accountInfo.Email) ? accountInfo.Email : accountInfo.MobilePhone,
        //                    password = RSAHelp.RSAEncrypt(accountInfo.Password)
        //                },
        //                success = true
        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new
        //            {
        //                data = new
        //                {
        //                    username = RSAHelp.RSAEncrypt(!string.IsNullOrEmpty(accountInfo.Email) ? accountInfo.Email : accountInfo.MobilePhone),
        //                    password = RSAHelp.RSAEncrypt(accountInfo.Password)
        //                },
        //                success = true
        //            }, JsonRequestBehavior.AllowGet);
        //        }

        //    }
        //    else
        //    {
        //        return Json(new
        //        {
        //            error_code = 10008,
        //            error_msg = "账号对应的用户不存在",
        //            success = false
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        #endregion

        #region  移除用户所有应用token

        public ActionResult Remove_Token()
        {
            var accountId = Request["aid"];
            var token = Request["token"];

            AccountBusiness.RemoveAccessToken(token,accountId);

            //var tokenList = MD.Business.SQL.OAuth2AccessToken.GetAccountAllToken(accountId);

            //tokenList.ForEach((a) =>
            //{
            //    MD.Caching.ApiCache.RemoveAccessToken(a.AccessToken);
            //});

            //MD.Business.SQL.OAuth2AccessToken.RemoveAccountToken(accountId);

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
    }
}