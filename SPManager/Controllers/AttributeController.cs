using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Product.DTO;
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
    public class AttributeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "属性管理";
            return View();
        }
        public JsonResult AddAttribute(AttributeDto attribute)
        {
            IAttributeAppService service = IocManager.Instance.Resolve<IAttributeAppService>();
            var result = service.AddAttribute(attribute);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UpdaeAttributeDisplaySequence(int attributeId, int displaySequence)
        {
            IAttributeAppService service = IocManager.Instance.Resolve<IAttributeAppService>();
            var result = service.UpdaeAttributeDisplaySequence(attributeId, displaySequence);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteAttribute(int attributeId)
        {
            IAttributeAppService service = IocManager.Instance.Resolve<IAttributeAppService>();
            var result = service.DeleteAttribute(attributeId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAttributeList(int pageIndex, int pageSize)
        {
            IAttributeAppService service = IocManager.Instance.Resolve<IAttributeAppService>();
            var result = service.GetAttributeList(pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.GetAttributeListCount();
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

        public JsonResult SearchAttributeByName(string attributeName, int pageIndex, int pageSize)
        {
            IAttributeAppService service = IocManager.Instance.Resolve<IAttributeAppService>();
            var result = service.SearchAttributeByName(attributeName, pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.SearchAttributeByNameCount(attributeName);
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

        public JsonResult EditAttribute([FromBody]AttributeDto attribute)
        {
            IAttributeAppService service = IocManager.Instance.Resolve<IAttributeAppService>();
            var result = service.EditAttribute(attribute);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetAttributeDetail(int id)
        {
            IAttributeAppService service = IocManager.Instance.Resolve<IAttributeAppService>();
            var result = service.GetAttributeDetail(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
