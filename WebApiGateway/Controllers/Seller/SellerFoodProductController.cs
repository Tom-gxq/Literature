using ProductGRPCInterface;
using SP.Api.Model.Seller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebApiGateway.Controllers.Base;

namespace WebApiGateway.Controllers.Seller
{
    public class SellerFoodProductController : BaseController
    {
        public ActionResult SelectSellerProduct(int supplierProductId, bool isSelected)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var ret = SellerProductBusiness.SelectSellerProduct(supplierProductId, currentAccount.AccountId, isSelected);
                if (ret)
                {
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 1);
                }
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult UpdateSupplierProductSaleStatus(string productId, int suppliersId, int saleStatus)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var ret = SellerProductBusiness.UpdateSupplierProductSaleStatus(productId, suppliersId, saleStatus);
                if (ret)
                {
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 1);
                }
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult AddSuppliersProduct(AddSuppliersProductModel model)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var ret = SellerProductBusiness.AddSuppliersProduct(currentAccount.AccountId, model.ProductId, 
                    model.SuppliersId,model.PurchasePrice,model.mainType,model.secondType);
                if (ret)
                {
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 1);
                }
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }
        public ActionResult UpdateSuppliersProduct(string productId, int suppliersId, double purchasePrice)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var ret = SellerProductBusiness.UpdateSuppliersProduct( productId,  suppliersId, purchasePrice);
                if (ret)
                {
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 1);
                }
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult GetSuppliersProductById(int suppliersPrductId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = SellerProductBusiness.GetSuppliersProductById(suppliersPrductId);
                JsonResult.Add("product", model);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult GetSuppliersType(int suppliersId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = SellerProductBusiness.GetSuppliersType(suppliersId);
                JsonResult.Add("productType", model);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult GetSupplierInfo()
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = SellerProductBusiness.GetSupplierInfo(currentAccount.AccountId);
                JsonResult.Add("supplier", model);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult GetSuppliersProducts(int mainType, int secondType, int suppliersId)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = SellerProductBusiness.GetSuppliersProducts( mainType,  secondType,  suppliersId);
                JsonResult.Add("supplierProductList", model);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult GetSellerFoodProductList(int regionId,  bool isSelected, int pageIndex, int pageSize)
        {
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var model = SellerProductBusiness.GetSellerFoodProductList(regionId, currentAccount.AccountId, isSelected, pageIndex, pageSize);
                JsonResult.Add("supplierProductList", model);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("error_msg", ex.Message);
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }
    }
}