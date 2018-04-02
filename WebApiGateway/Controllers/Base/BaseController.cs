using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SP.Api.Cache;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using WebApiGateway.App_Start;
using WebApiGateway.Models.Token;

namespace WebApiGateway.Controllers.Base
{
    public class BaseController : Controller
    {
        public string accountID = string.Empty;
        public string accessToken = string.Empty;
        public bool EGroup = false;
        public DateTime startTime;
        public AccountModel currentAccount = null;
        public ApiEnum.ErrorCode errorCode = ApiEnum.ErrorCode.Success;
        protected Dictionary<string, object> JsonResult = new Dictionary<string, object>();

        public string errorMsg = string.Empty;
          
        protected override void Initialize(RequestContext controllerContext)
        {
            try
            {
                var Context = controllerContext.HttpContext;
                var Request = Context.Request;//定义传统request对象

                string access_token = Request["access_token"];
                
                startTime = DateTime.Now;

                if (string.IsNullOrEmpty(access_token))
                    errorCode = ApiEnum.ErrorCode.BaseBadToken;
                else
                {
                    //根据token获取授权用户信息
                    TokenModel tokenModel = TokenCache.Get(access_token);

                    if (string.IsNullOrEmpty(tokenModel.Access_Token))
                        errorCode = ApiEnum.ErrorCode.BaseBadToken;
                    else
                    {
                        DateTime expires = DateTime.MaxValue;
                        accessToken = tokenModel.Access_Token;
                        string accessTokenExpires = tokenModel.Access_Token_Expires;
                        
                        if (!string.IsNullOrEmpty(accessTokenExpires))
                        {
                            expires = DateTime.Parse(accessTokenExpires);
                        }                            

                        if (expires >= DateTime.Now)
                        {
                            accountID = tokenModel.AccountId;

                            currentAccount = AccountInfoCache.GetAccountInfoByAccountId(accountID);
                            
                            if (currentAccount == null)
                            {
                                errorCode = ApiEnum.ErrorCode.BaseBadUser;
                            }
                        }
                        else
                            errorCode = ApiEnum.ErrorCode.BaseDisabledToken;
                    }
                }
            }
            catch (Exception ex)
            {
                errorCode = ApiEnum.ErrorCode.ComBad;
                Common.WriteLog("ex:" + ex.ToString()+"\r\n statck:"+ex.StackTrace);
            }

            var routeData = controllerContext.RouteData;
            //返回错误
            if (errorCode != ApiEnum.ErrorCode.Success)
            {
                routeData.Values["action"] = "BaseNotNext";
                routeData.Values["error_code"] = errorCode;
                //IHttpController httpController = new ErrorController();
                //controllerContext.Controller = httpController;
                //controllerContext.ControllerDescriptor = new HttpControllerDescriptor(controllerContext.Configuration, "Error", httpController.GetType());

            }
            else
            {
                JObject requestData = new JObject{
                {"accountID",currentAccount.AccountId},               
                {"token",accessToken},
                {"startTime",startTime}
                };
                //添加LOG日志参数
                routeData.Values["request_data"] = JsonConvert.SerializeObject(requestData);
            }

            base.Initialize(controllerContext);
        }
        public JObject BaseNotNext(int error_code)
        {
            JObject resultObj = new JObject();
            resultObj.Add("error_code", error_code );
            resultObj.Add("status", 3);//token验证失败

            return resultObj;
        }
    }
}
