using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Seller;
using SP.Application.Seller.DTO;
using SP.Application.Suppler;
using SPManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class SellerController : BaseController
    {
        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Region()
        {
            return View();
        }

        public ActionResult Product()
        {
            return View();
        }

        public ActionResult Leader()
        {
            return View();
        }

        public JsonResult SearchRegionListByKeyWord(string keyWord, int pageIndex, int pageSize)
        {
            //IOrderAppService service = IocManager.Instance.Resolve<IOrderAppService>();
            //var list = service.SearchOrderListByKeyWord(keyWord, pageIndex, pageSize);
            //IProductAppService productService = IocManager.Instance.Resolve<IProductAppService>();
            //foreach (var item in list)
            //{
            //    item.ProductList = productService.GetProductListByOrderId(item.OrderId);
            //    foreach (var pitem in item.ProductList)
            //    {
            //        string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
            //        foreach (var pimg in pitem.ProductImage)
            //        {
            //            if (!string.IsNullOrEmpty(pimg.ImgPath) && !string.IsNullOrEmpty(domain))
            //            {
            //                pimg.ImgPath = domain + pimg.ImgPath;
            //            }
            //        }
            //    }
            //}

            //JsonResult.Add("result", list);

            //return new JsonResult()
            //{
            //    Data = JsonResult,
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //};

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetSellerRegion()
        {
            ISupplerAppService supplerSrv = IocManager.Instance.Resolve<ISupplerAppService>();
            var sellers = supplerSrv.GetSupplerList();
            JsonResult.Add("sellers", sellers);
            IRegionAppService regionSrv = IocManager.Instance.Resolve<IRegionAppService>();
            var regions = regionSrv.GetRegionData(1);
            JsonResult.Add("regions", regions);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetSellerList(int pageIndex, int pageSize)
        {
            var service = IocManager.Instance.Resolve<ISuppliersRegionService>();
            var list = service.GetSuppliersRegionList(pageIndex, pageSize);
            var total = service.GetSuppliersRegionCount();
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

        public JsonResult GetRegionList(int pageIndex, int pageSize)
        {
            var service = IocManager.Instance.Resolve<ISuppliersRegionService>();
            var list = service.GetSuppliersRegionList(pageIndex, pageSize);
            var total = service.GetSuppliersRegionCount();
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

        public JsonResult GetRegionDetail(int id)
        {
            ISupplerAppService supplerSrv = IocManager.Instance.Resolve<ISupplerAppService>();
            var sellers = supplerSrv.GetSupplerList();
            JsonResult.Add("sellers", sellers);
            IRegionAppService regionSrv = IocManager.Instance.Resolve<IRegionAppService>();
            var regions = regionSrv.GetRegionData(1);
            JsonResult.Add("regions", regions);
            ISuppliersRegionService suppliersSrv = IocManager.Instance.Resolve<ISuppliersRegionService>();
            var supplerDetail = suppliersSrv.GetSupplerDetail(id);
            JsonResult.Add("SuppliersId", supplerDetail?.SuppliersId);
            JsonResult.Add("RegionId", supplerDetail?.RegionID);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public void AddRegion(int sellerId, int regionId)
        {
            ISuppliersRegionService suppliersSrv = IocManager.Instance.Resolve<ISuppliersRegionService>();
            suppliersSrv.AddSeller(new SuppliersRegionDto() { SuppliersId = sellerId, RegionID = regionId });
        }

        public void EditRegion(int id, int sellerId, int regionId)
        {
            ISuppliersRegionService suppliersSrv = IocManager.Instance.Resolve<ISuppliersRegionService>();
            suppliersSrv.UpdateSeller(new SuppliersRegionDto() { Id = id, SuppliersId = sellerId, RegionID = regionId });
        }
    }
}