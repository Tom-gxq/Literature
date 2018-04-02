using LibMain.Dependency;
using Newtonsoft.Json.Linq;
using SP.Application.User;
using SP.Application.User.DTO;
using SPManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class AdminController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "人员管理";
            return View();
        }
        public ActionResult LogOut()
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            service.DelCurrentSession();
            return Redirect("/Home");
        }
        // GET: api/Admin/5
        public JsonResult CheckAdminLogin(string userName,string passWord)
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            var result = service.CheckAdminLogin(userName, passWord);
            JsonResult.Add("result", result);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AddAdmin([FromBody]string userName, [FromBody]string passWord)
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            var result = service.AddAdmin(userName, passWord);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DelAdmin(int id)
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            var result = service.DelAdmin(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAdminList(int pageIndex, int pageSize)
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            var result = service.GetAdminList(pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.GetAdminListCount();
            PageModel jObject = new PageModel();
            jObject.Total = total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SearchAdminByUserName(string userName, int pageIndex, int pageSize)
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            var result = service.SearchAdminByUserName(userName);
            JsonResult.Add("result", result);
            PageModel jObject = new PageModel();
            var total = service.SearchAdminByUserNameCount(userName);
            jObject.Total = total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAdminDetail(int id)
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            var result = service.GetAdminDetail(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditAdmin(AdminDto admin)
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            var result = service.EditAdmin(admin);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
