using LibMain.Dependency;
using SP.Application.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public JsonResult SearchAccount(string keywords)
        {
            IAccountAppService service = IocManager.Instance.Resolve<IAccountAppService>();
            var list = service.SearchAccount(keywords);
            JsonResult.Add("items", list);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}