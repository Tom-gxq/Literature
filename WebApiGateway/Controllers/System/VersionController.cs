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
                url = "https://ejiajunapp.oss-cn-beijing.aliyuncs.com/app-release.apk",
                updateMessage = "更新购物车计算",
                versionCode = "1.0.1"
            };
            return model;
        }
    }
}
