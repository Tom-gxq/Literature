using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgentDashboard.Controllers
{
    public class QiniuController : Controller
    {
        // GET: Qiniu
        public ActionResult GetUpToken()
        {
            Mac mac = new Mac("_pnah4FIzvPiGeMTMOcUsZ_FCtu9t6qiDzRwLSgI", "53noQ6jV4RJ-35-8LVCB8VohLjokW1LZd6rRP25M");
            PutPolicy putPolicy = new PutPolicy();
            putPolicy.Scope = "qtestbucket";
            string token = Auth.CreateUploadToken(mac, putPolicy.ToJsonString());

            var result = new Dictionary<string, object>();
            result.Add("uptoken", token);
            return new JsonResult()
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}