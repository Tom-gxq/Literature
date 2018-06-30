using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Shop;
using SP.Application.Shop.DTO;
using SP.Application.User;
using SPManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class ShopController : BaseController
    {
        // GET: Shop
        public ActionResult Index()
        {
            ViewBag.Title = "店铺管理";
            return View();
        }

        public JsonResult AddShop(string shopName, int displaySequence,string startTime,string endTime,int shopType,string shopLogo)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.AddShop(shopName, displaySequence, startTime, endTime, shopType, shopLogo);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DelShop(int id)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.DelShop(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetShopList(int pageIndex, int pageSize)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.GetShopList(pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.GetShopListCount();
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

        public JsonResult SearchShopByUserName(string keyWord, int pageIndex=1, int pageSize=1000)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.SearchShopByUserName(keyWord, pageIndex, pageSize);
            JsonResult.Add("result", result);
            PageModel jObject = new PageModel();
            var total = service.SearchShopByUserNameCount(keyWord);
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

        public JsonResult GetShopDetail(int id)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.GetShopDetail(id);
            JsonResult.Add("result", result);
            IAttributeAppService typeService = IocManager.Instance.Resolve<IAttributeAppService>();
            var typeList = typeService.GetAttributeList(1,20);
            JsonResult.Add("attribute", typeList);
            IRegionAppService regionService = IocManager.Instance.Resolve<IRegionAppService>();
            var schoolList = regionService.GetRegionData(1);
            JsonResult.Add("school", schoolList);            
            var areaList = regionService.GetRegionData(2);
            JsonResult.Add("area", areaList);
            IProductTypeService pservice = IocManager.Instance.Resolve<IProductTypeService>();
            var ptype = pservice.GetAllProductTypeList(0);
            JsonResult.Add("productType", ptype);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditShop(ShopDto admin)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.EditShop(admin);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult ProductManager()
        {
            ViewBag.Title = "店铺产品管理";
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var list = service.GetAllProductTypeList(0);
            ViewBag.TypeData = list;

            IBrandAppService brandService = IocManager.Instance.Resolve<IBrandAppService>();
            var brandList = brandService.GetAllBrandList();
            ViewBag.BrandData = brandList;
            return View();
        }

        public JsonResult AddShopProduct(int ShopId, string ProductId)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.AddShopProduct(ShopId, ProductId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DelShopProductByShopId(int ShopId)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.DelShopProductByShopId(ShopId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetShopListByRegionId(int regionId)
        {
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.GetShopListByRegionId(regionId);
            JsonResult.Add("items", result);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetFoodShopListByRegionId(int regionId)
        {
            string marketId = ConfigurationManager.AppSettings["MarketId"];
            int id = 0;
            int.TryParse(marketId, out id);
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.GetFoodShopListByRegionId(regionId, id);
            JsonResult.Add("items", result);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetMarketShopListByRegionId(int regionId)
        {
            string marketId = ConfigurationManager.AppSettings["MarketId"];
            int id = 0;
            int.TryParse(marketId, out id);
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var result = service.GetMarketShopListByRegionId(regionId, id);
            JsonResult.Add("items", result);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}