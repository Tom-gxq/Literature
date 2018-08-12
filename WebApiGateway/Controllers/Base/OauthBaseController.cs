using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApiGateway.App_Start.Enum;
using WebApiGateway.Models.Oauth2;
using WebApiGateway.Models.Token;

namespace WebApiGateway.Controllers.Base
{
    public class OauthBaseController : Controller
    {
        public string redirect_uri,
            grantType = string.Empty;//分配到的App Key//授权回调地址，站外应用需与设置的回调地址一致 //授权回调地址，站外应用需与设置的回调地址一致 //请求Token类型 账号密码还是code
        public string response_type = "code";//返回类型，支持code、token，默认值为code。

        public string  accountId, format, state = string.Empty;
        public int userType;

        public AccountResult accountResult = AccountResult.AccountError;
        public TokenModel accessTokenModel = null;

        public string error_msg = string.Empty;
        public ApiEnum.ErrorCode error_code = ApiEnum.ErrorCode.Success;
        public static string appId = "";

        protected override void Initialize(RequestContext requestContext)
        {
            var context = requestContext.HttpContext;
            var request = context.Request;

            string access_token = request["access_token"];
            format = request["format"] ?? "json";

            if (!string.IsNullOrEmpty(access_token))
            {
                try
                {
                    //根据token获取授权用户信息
                    TokenModel tokenModel = TokenCache.Get(access_token);
                    

                    DateTime expires = DateTime.MaxValue;
                    string accessTokenExpires = tokenModel.Access_Token_Expires;

                    if (!string.IsNullOrEmpty(accessTokenExpires))
                    {
                        expires = DateTime.Parse(accessTokenExpires);
                    }

                    if (expires >= DateTime.Now)
                    {
                        accountId = tokenModel.AccountId;
                        userType = tokenModel.UserType;
                    }
                    else
                        error_code = ApiEnum.ErrorCode.BaseDisabledToken;
                }
                catch (Exception ex) { }
            }

            base.Initialize(requestContext);
        }
    }
}