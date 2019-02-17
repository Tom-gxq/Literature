using ProductGRPCInterface;
using SP.Api.Model.Product;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Seller
{
    public class SellerProductController : BaseController
    {
        public ActionResult GetMarketProduct(int typeId,  int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                string mainTypeConf = ConfigurationManager.AppSettings["MainType.Market"];
                var mainTypeId = int.Parse(mainTypeConf);
                var list = ProductBusiness.GetDistributorMarketProduct(mainTypeId, typeId, pageIndex, pageSize);
                string domainPath = ConfigurationManager.AppSettings["Qiniu.Domain"];
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.imagePath != null && !item.imagePath.ToLower().StartsWith("http://"))
                        {
                            item.imagePath = domainPath + item.imagePath;                            
                        }
                    }
                }
                JsonResult.Add("prouctList", list);
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
        public ActionResult GetFoodShopProductList(int typeId, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetDistributorFoodShopProductList(currentAccount.AccountId, typeId, pageIndex, pageSize);
                string domainPath = ConfigurationManager.AppSettings["Qiniu.Domain"];
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.imagePath != null && !item.imagePath.ToLower().StartsWith("http://"))
                        {
                            item.imagePath = domainPath + item.imagePath;
                        }
                    }
                }
                JsonResult.Add("prouctList", list);
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

        public ActionResult GetSellerMarketProduct(int typeId, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                string mainTypeConf = ConfigurationManager.AppSettings["MainType.Market"];
                var mainTypeId = int.Parse(mainTypeConf);
                var list = ProductBusiness.GetSellerMarketProduct(currentAccount.AccountId, mainTypeId, typeId, pageIndex, pageSize);
                string domainPath = ConfigurationManager.AppSettings["Qiniu.Domain"];
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.imagePath != null && !item.imagePath.ToLower().StartsWith("http://"))
                        {
                            item.imagePath = domainPath + item.imagePath;
                        }
                    }
                }
                JsonResult.Add("prouctList", list);
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
        public ActionResult GetSellerFoodShopProductList(int typeId, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                string mainTypeConf = ConfigurationManager.AppSettings["MainType.Food"];
                var mainTypeId = int.Parse(mainTypeConf);
                var list = ProductBusiness.GetSellerFoodShopProductList(currentAccount.AccountId, mainTypeId, typeId, pageIndex, pageSize);
                string domainPath = ConfigurationManager.AppSettings["Qiniu.Domain"];
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item.imagePath != null && !item.imagePath.ToLower().StartsWith("http://"))
                        {
                            item.imagePath = domainPath + item.imagePath;
                        }
                    }
                }
                JsonResult.Add("prouctList", list);
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

        public ActionResult GetAllProductTypeList(int kind=0)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetAllProductTypeList(kind);
                JsonResult.Add("productTypeList", list);
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

        public ActionResult AddProduct([FromBody]SellerProductModel product)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                product.accountId = currentAccount.AccountId;
                var list = ProductBusiness.AddProduct(product);
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

        public ActionResult GetProductDetail(string productId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = ProductBusiness.GetSellerProductDetail(productId);
                JsonResult.Add("product", model);
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

        public ActionResult UpdateProduct([FromBody]SellerProductModel product)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                product.accountId = currentAccount.AccountId;
                var list = ProductBusiness.UpdateProduct(product);
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

        public ActionResult UpdateProductSaleStatus(string productId,int status)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.UpdateProductSaleStatus(productId, status);
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

        public ActionResult AddSuppliersProduct(string productId, string accountId, int stock, double purchasePrice)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var ret = ProductBusiness.AddSuppliersProduct( productId,  accountId,  stock,  purchasePrice);
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