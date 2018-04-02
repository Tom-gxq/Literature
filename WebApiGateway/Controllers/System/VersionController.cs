using SP.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Models.Version;

namespace WebApiGateway.Controllers
{
    public class VersionController : ApiController
    {
        [System.Web.Mvc.HttpGet]
        public VersionModel GetCurrentVerion()
        {
            var model = new VersionModel()
            {
                url = "http://www.ejiajunxy.cn/apk/app-debug.apk",
                updateMessage = "美哟内容",
                versionCode = "1.0.0"
            };
            return model;
        }
    }
}
