using LibMain.Dependency;
using SP.Application.User;
using SP.Application.User.DTO;
using SPManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class AssociatorController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "会员管理";
            return View();
        }
        public JsonResult AddAssociator(AssociatorDto associator)
        {
            var service = IocManager.Instance.Resolve<IAssociatorAppService>();
            var result = service.AddAssociator(associator);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteAssociator(string associatorId)
        {
            var service = IocManager.Instance.Resolve<IAssociatorAppService>();
            var result = service.DeleteAssociator(associatorId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAssociatorList(int pageIndex, int pageSize)
        {
            var service = IocManager.Instance.Resolve<IAssociatorAppService>();
            var result = service.GetAssociatorList(pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.GetAssociatorListCount();
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
        public JsonResult SearchAssociatorByAccountId(string accountId, int pageIndex, int pageSize)
        {
            var service = IocManager.Instance.Resolve<IAssociatorAppService>();
            var result = service.SearchAssociatorByAccountId(accountId,pageIndex, pageSize);
            JsonResult.Add("result", result);
            PageModel jObject = new PageModel();
            jObject.Total = 1;
            jObject.Pages = 1;
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}