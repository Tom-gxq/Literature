using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using SPManager.Models;

namespace SPManager.Controllers
{
    public class BrandController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "品牌管理";
            return View();
        }
        public JsonResult AddBrand(string brandName,int displaySequence)
        {
            IBrandAppService service = IocManager.Instance.Resolve<IBrandAppService>();
            var result = service.AddBrand(new BrandDto()
            {
               BrandName = brandName,
               DisplaySequence = displaySequence
            });
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UpdateBrandDisplaySequence(int brandId, int displaySequence)
        {
            IBrandAppService service = IocManager.Instance.Resolve<IBrandAppService>();
            var result = service.UpdateBrandDisplaySequence(brandId, displaySequence);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteBrand(int brandId)
        {
            IBrandAppService service = IocManager.Instance.Resolve<IBrandAppService>();
            var result = service.DeleteBrand(brandId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetBrandList(int pageIndex, int pageSize)
        {
            IBrandAppService service = IocManager.Instance.Resolve<IBrandAppService>();
            var result = service.GetBrandList(pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.GetBrandListCount();
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

        public JsonResult SearchBrandByName(string brandName, int pageIndex, int pageSize)
        {
            IBrandAppService service = IocManager.Instance.Resolve<IBrandAppService>();
            var result = service.SearchBrandByName(brandName, pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.SearchBrandByNameCount(brandName);
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

        public JsonResult EditBrand([FromBody]BrandDto productType)
        {
            IBrandAppService service = IocManager.Instance.Resolve<IBrandAppService>();
            var result = service.EditBrand(productType);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetBrandDetail(int id)
        {
            IBrandAppService service = IocManager.Instance.Resolve<IBrandAppService>();
            var result = service.GetBrandDetail(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
