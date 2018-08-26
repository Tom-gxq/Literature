using AccountGRPCInterface;
using ProductGRPCInterface;
using SP.Api.Model;
using SP.Api.Model.Product;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Product
{
    public class ProductController : BaseController
    {
        [System.Web.Mvc.HttpGet]
        public ActionResult GetAllShopList(int districtId,int shopType,int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetAllShopList(districtId, shopType, pageIndex, pageSize);
                JsonResult.Add("shopList", list);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult GetTitleAttributeList()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetTitleAttributeList();
                JsonResult.Add("attributeList", list);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult GetShopProductList(int districtId, long typeId,  int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                List<ProductModel> retList = new List<ProductModel>();
                var list = ProductBusiness.GetShopProductList(districtId, typeId, pageIndex, pageSize);
                if (list != null)
                {
                    string domainPath = ConfigurationManager.AppSettings["Qiniu.Domain"];
                    var groupResult = list.GroupBy(x => x.productId);
                    foreach (var item in groupResult)
                    {
                        var product = item.FirstOrDefault();
                        if (product != null)
                        {
                            product.skuNum = item.Sum(x => x.skuNum);
                            if (product.images != null)
                            {
                                foreach (var img in product.images)
                                {
                                    img.imgPath = !string.IsNullOrEmpty(img.imgPath) ? (domainPath + img.imgPath) : string.Empty;
                                }
                            }
                            retList.Add(product);
                        }
                    }
                }
                JsonResult.Add("shopProductList", retList);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult GetFoodShopProductList(int districtId, int shopId, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetFoodShopProductList(districtId, shopId, pageIndex, pageSize);
                string domainPath = ConfigurationManager.AppSettings["Qiniu.Domain"];
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.images != null)
                        {
                            foreach (var img in item.images)
                            {
                                img.imgPath = !string.IsNullOrEmpty(img.imgPath) ? (domainPath + img.imgPath) : string.Empty;
                            }
                        }
                    }
                }
                JsonResult.Add("shopProductList", list);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult GetShopById(int shopId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetShopById(shopId);
                JsonResult.Add("shopList", list);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        public ActionResult GetCarouselList()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetCarouselList();
                JsonResult.Add("list", list);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult GetTitleTypeList()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetTitleTypeList();
                JsonResult.Add("titleTypeList", list);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        [System.Web.Mvc.HttpGet]
        public ActionResult GetProductTypeTitleList()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetProductTypeTitleList();
                JsonResult.Add("titleTypeList", list);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
    }
}