using SP.Api.Cache;
using SP.Api.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using WebApiGateway.App_Start;
using WebApiGateway.Models.Token;

namespace WebApiGateway.Controllers.Base
{
    public class Test : ApiController
    {
        public string accountID = string.Empty;
        public string accessToken = string.Empty;
        public bool EGroup = false;
        public DateTime startTime;
        public AccountModel currentAccount = null;
        public ApiEnum.ErrorCode errorCode = ApiEnum.ErrorCode.Success;

        public string errorMsg = string.Empty;
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            try
            {
                
                Common.WriteLog("access_token:" + "");
                var requst = controllerContext.Request;
                startTime = DateTime.Now;



            }
            catch (Exception ex)
            {
                
            }
            base.Initialize(controllerContext);
        }
    }
}