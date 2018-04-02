using LibMain.Dependency;
using SP.Application.Discount;
using SP.Application.Discount.DTO;
using SP.Application.Product;
using SPManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class DiscountController : BaseController
    {
        // GET: Discount
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetSysKind(int kind, int pageIndex, int pageSize)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var list = service.GetDiscountList(kind, pageIndex, pageSize);
            var total = service.GetOrderListCount(kind);
            JsonResult.Add("items", list);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 添加会员优惠折扣
        /// </summary>
        /// <param name="sysKind"></param>
        /// <returns></returns>
        public JsonResult AddAssociator([FromBody]SysKindDto sysKind)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.AddSysKind(sysKind,100);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult AddCoupons([FromBody]SysKindDto sysKind)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.AddSysKind(sysKind, 200);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetAssociatorDetail(string kindId)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.GetAssociatorDetail(kindId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditSysKind([FromBody]SysKindDto sysKind)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.EditSysKind(sysKind);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteSysKind(string kindId)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.DeleteSysKind(kindId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult CouponsIndex()
        {
            return View();
        }
        public ActionResult ResEventIndex()
        {
            return View();
        }
        public JsonResult GetResEvent(int pageIndex, int pageSize)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var list = service.GetResEventList(pageIndex, pageSize);
            var total = service.GetResEventListCount();
            JsonResult.Add("items", list);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult AddResEvent([FromBody]ResEventDto resEvent)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.AddResEvent(resEvent);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DeleteResEvent(int id)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.DeleteResEvent(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult EventSettingIndex()
        {
            return View();
        }
        public JsonResult GetEventList(int pageIndex, int pageSize)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var list = service.GetEventList(pageIndex, pageSize);
            var total = service.GetEventListCount();
            JsonResult.Add("items", list);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult AddEvent([FromBody]EventDto resEvent)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.AddEvent(resEvent);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DeleteEvent(int id)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.DeleteEvent(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetEventDetail()
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var sysEvent = service.GetSysEventList();
            JsonResult.Add("sysEvent", sysEvent);           
            var resEventList = service.GetResEventList();
            JsonResult.Add("resEvent", resEventList);
            var result = new Dictionary<string, object>();
            result.Add("result", JsonResult);
            return new JsonResult()
            {
                Data = result,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetSysKindList(int resEventId)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var sysKind = service.GetSysKindList(resEventId);
            JsonResult.Add("items", sysKind);            
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult CarouselIndex()
        {
            return View();
        }
        public JsonResult GetCarouselList(int pageIndex, int pageSize)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var list = service.GetCarouselList(pageIndex, pageSize);
            var total = service.GetCarouselListCount();
            JsonResult.Add("items", list);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult AddCarousel([FromBody]CarouselDto carousel)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.AddCarousel(carousel);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DeleteCarousel(int id)
        {
            IDiscountAppService service = IocManager.Instance.Resolve<IDiscountAppService>();
            var result = service.DeleteCarousel(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}