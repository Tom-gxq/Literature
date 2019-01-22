﻿using LibMain.Dependency;
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

        public JsonResult SearchRegionByName(string supplierName, int pageIndex, int pageSize)
        {
            ISuppliersRegionService service = IocManager.Instance.Resolve<ISuppliersRegionService>();
            var result = service.SearchRegionByName(supplierName);
            JsonResult.Add("result", result);
            PageModel jObject = new PageModel();
            var total = service.SearchRegionByNameCount(supplierName);
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

        public JsonResult AddRegion(int sellerId, int regionId)
        {
            ISuppliersRegionService suppliersSrv = IocManager.Instance.Resolve<ISuppliersRegionService>();
            var result = suppliersSrv.AddSeller(new SuppliersRegionDto() { SuppliersId = sellerId, RegionID = regionId });
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditRegion(int id, int sellerId, int regionId)
        {
            ISuppliersRegionService suppliersSrv = IocManager.Instance.Resolve<ISuppliersRegionService>();
            var result = suppliersSrv.UpdateSeller(new SuppliersRegionDto() { Id = id, SuppliersId = sellerId, RegionID = regionId });
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DelRegion(int id)
        {
            ISuppliersRegionService suppliersSrv = IocManager.Instance.Resolve<ISuppliersRegionService>();
            var result = suppliersSrv.DelSeller(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetLeaderList(int pageIndex, int pageSize)
        {
            var service = IocManager.Instance.Resolve<IRegionAccountService>();
            var list = service.GetLeaderList(pageIndex, pageSize);
            var total = service.GetLeaderCount();
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
    }
}