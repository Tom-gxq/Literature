using ProductGRPCInterface;
using SP.Api.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Seller
{
    public class SellerProductController : BaseController
    {
        public ActionResult GetMarketProduct(int dormId,int typeId, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetDistributorMarketProduct(dormId, typeId, pageIndex, pageSize);
                JsonResult.Add("accountInfo", list);
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
        public ActionResult GetFoodShopProductList(int dormId, int typeId, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetDistributorFoodShopProductList(dormId, typeId, pageIndex, pageSize);
                JsonResult.Add("accountInfo", list);
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

        public ActionResult GetSellerMarketProduct(int dormId, int typeId, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetSellerMarketProduct(dormId, typeId, pageIndex, pageSize);
                JsonResult.Add("accountInfo", list);
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
        public ActionResult GetSellerFoodShopProductList(int dormId, int typeId, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.GetSellerFoodShopProductList(dormId, typeId, pageIndex, pageSize);
                JsonResult.Add("accountInfo", list);
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
                JsonResult.Add("accountInfo", list);
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
    }
}