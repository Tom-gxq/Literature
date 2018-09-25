using SP.Api.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            string versionCode = ConfigurationManager.AppSettings["APP.VersionCode"];
            string updateMessage = ConfigurationManager.AppSettings["APP.UpdateMessage"];
            string url = ConfigurationManager.AppSettings["APP.Url"];
            var model = new VersionModel()
            {
                url = (!string.IsNullOrEmpty(url)) ? url:"http://server.m.pp.cn/download/apk?appId=7712427&custom=0&ch_src=pp_dev&ch=default",
                updateMessage = (!string.IsNullOrEmpty(updateMessage)) ? updateMessage : "全新UI设计，更加美观舒心",
                versionCode = (!string.IsNullOrEmpty(versionCode)) ? versionCode : "2.0.0"
            };
            return model;
        }

        [System.Web.Mvc.HttpGet]
        public VersionModel GetSellerCurrentVerion()
        {
            string versionCode = ConfigurationManager.AppSettings["Seller.APP.VersionCode"];
            string updateMessage = ConfigurationManager.AppSettings["Seller.APP.UpdateMessage"];
            string url = ConfigurationManager.AppSettings["Seller.APP.Url"];
            var model = new VersionModel()
            {
                url = (!string.IsNullOrEmpty(url)) ? url : "http://server.m.pp.cn/download/apk?appId=7712427&custom=0&ch_src=pp_dev&ch=default",
                updateMessage = (!string.IsNullOrEmpty(updateMessage)) ? updateMessage : "全新UI设计，更加美观舒心",
                versionCode = (!string.IsNullOrEmpty(versionCode)) ? versionCode : "2.0.0"
            };
            return model;
        }
    }
}
