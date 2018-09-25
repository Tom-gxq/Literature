using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Product.DTO;
using SP.Application.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using SPManager.Models;
using System.Configuration;

namespace SPManager.Controllers
{
    public class ProductTypeController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "分类管理";
            return View();
        }
        public JsonResult AddProductType(string typeName, int displaySequence,string typePath,string typeLogo,int kind)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.AddProductType(typeName, displaySequence, typePath,typeLogo,kind);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DelProductType(int id)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.DelProductType(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductTypeList(int kind,int pageIndex, int pageSize)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.GetProductTypeList(kind,pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.GetProductTypeListCount();
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

        public JsonResult GetProductTypeParentList(int parentId)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            string marketTypeId = ConfigurationManager.AppSettings["MarketTypeId"];
            int kind = 0;
            if(parentId.ToString() == marketTypeId)
            {
                kind = 1;
            }
            else
            {
                kind = 2;
            }
            var result = service.GetProductTypeList(kind);
            JsonResult.Add("result", result);
            
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SearchProductTypeByName(string typeName, int pageIndex, int pageSize)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.SearchProductTypeByName(typeName, pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.SearchProductTypeByNameCount(typeName);
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
        public JsonResult SearchProductTypeByKeyword(string keyword)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.SearchProductTypeByName(keyword, 1, 30);
            JsonResult.Add("items", result);
            
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditProductType([FromBody]ProductTypeDto productType)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.EditProductType(productType);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteProductTypeById(int id)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.DeleteProductTypeById(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductTypeDetail(int id)
        {
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.GetProductTypeDetail(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
