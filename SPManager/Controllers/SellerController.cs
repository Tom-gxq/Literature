﻿using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Seller;
using SP.Application.Seller.DTO;
using SP.Application.Suppler;
using SP.Application.Suppler.DTO;
using SP.Application.User;
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

        public ActionResult Product(int Id)
        {
            return View(Id);
        }

        public ActionResult Leader()
        {
            return View();
        }

        public JsonResult SearchRegionByName(string supplierName, int pageIndex, int pageSize)
        {
            ISuppliersRegionService service = IocManager.Instance.Resolve<ISuppliersRegionService>();
            var result = service.SearchRegionByName(supplierName, pageIndex, pageSize);
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
            var service = IocManager.Instance.Resolve<ISupplerAppService>();
            List<dynamic> list = new List<dynamic>();
            var sellerList = service.GetSupplerList(pageIndex, pageSize)
                .OrderByDescending(x => x.CreateTime);
            foreach (var item in sellerList)
            {
                var acccountSrv = IocManager.Instance.Resolve<IAccountAppService>();
                var accountInfo = acccountSrv.GetAccountInfo(item?.AccountId);
                list.Add(new
                {
                    Id = item.Id,
                    SuppliersName = item?.SuppliersName ?? string.Empty,
                    AccountName = accountInfo?.Fullname ?? string.Empty,
                    State = item?.Status == 0 ? "营业" : "停业"
                });
            }

            var total = service.GetSellerCount();
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

        public JsonResult GetLeaderDetail(int regionId, string accountId)
        {
            IRegionAppService regionSrv = IocManager.Instance.Resolve<IRegionAppService>();
            var regions = regionSrv.GetRegionData(1);
            JsonResult.Add("regions", regions);
            IAccountAppService accountSrv = IocManager.Instance.Resolve<IAccountAppService>();
            var accountInfo = accountSrv.GetAccountInfo(accountId);
            JsonResult.Add("AccountId", accountId);
            JsonResult.Add("FullName", accountInfo.Fullname);
            JsonResult.Add("RegionId", regionId);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetRegionAccount()
        {
            IRegionAppService regionSrv = IocManager.Instance.Resolve<IRegionAppService>();
            var regions = regionSrv.GetRegionData(1);
            JsonResult.Add("regions", regions);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditLeader(int oldRegionId, string oldAccountId, int regionId, string accountId)
        {
            IRegionAccountService accountSrv = IocManager.Instance.Resolve<IRegionAccountService>();
            var result = accountSrv.UpdateLeader(new RegionAccountDto() { RegionId = oldRegionId, AccountId = oldAccountId },
                new RegionAccountDto() { RegionId = regionId, AccountId = accountId });
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DelLeader(int regionId, string accountId)
        {
            IRegionAccountService accountSrv = IocManager.Instance.Resolve<IRegionAccountService>();
            var result = accountSrv.DelLeader(regionId, accountId);
            JsonResult.Add("result", result);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AddLeader(int regionId, string accountId)
        {
            IRegionAccountService accountSrv = IocManager.Instance.Resolve<IRegionAccountService>();
            accountSrv.AddLeader(new RegionAccountDto() { RegionId = regionId, AccountId = accountId, Status = 0 });
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SearchLeaderByName(string leaderName, int pageIndex, int pageSize)
        {
            IRegionAccountService service = IocManager.Instance.Resolve<IRegionAccountService>();
            var result = service.SearchLeaderByName(leaderName, pageIndex, pageSize);
            JsonResult.Add("result", result);
            PageModel jObject = new PageModel();
            var total = service.SearchLeaderByNameCount(leaderName);
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

        public JsonResult AddSeller(SupplerDto model)
        {
            model.CreateTime = DateTime.Now;
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            bool result = service.AddSuppler(model);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DelSeller(int id)
        {
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            bool result = service.DelSupplerById(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetSellerDetail(int id)
        {
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var seller = service.GetSupplerDetail(id);
            JsonResult.Add("seller", seller);
            IAccountAppService accountSrv = IocManager.Instance.Resolve<IAccountAppService>();
            var accountInfo = accountSrv.GetAccountInfo(seller.AccountId);
            JsonResult.Add("AccountId", accountInfo.AccountId);
            JsonResult.Add("FullName", accountInfo.Fullname);
            JsonResult.Add("result", seller);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditSeller(SupplerDto model)
        {
            model.UpdateTime = DateTime.Now;
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var sellerInfo = service.GetSupplerDetail(model.Id);
            model.CreateTime = sellerInfo.CreateTime;
            model.LogoPath = model.LogoPath ?? sellerInfo.LogoPath;
            model.PermitPath = model.PermitPath ?? sellerInfo.PermitPath;
            model.LicensePath = model.LicensePath ?? sellerInfo.LicensePath;
            model.AuthorizationPath = model.AuthorizationPath ?? sellerInfo.AuthorizationPath;


            bool result = service.UpdateSeller(model);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditSellerLicense(SupplerDto model)
        {
            model.UpdateTime = DateTime.Now;
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var sellerInfo = service.GetSupplerDetail(model.Id);
            sellerInfo.LicensePath = model.LicensePath ?? sellerInfo.LicensePath;

            bool result = service.UpdateSeller(sellerInfo);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditSellerPermit(SupplerDto model)
        {
            model.UpdateTime = DateTime.Now;
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var sellerInfo = service.GetSupplerDetail(model.Id);
            sellerInfo.PermitPath = model.PermitPath ?? sellerInfo.PermitPath;

            bool result = service.UpdateSeller(sellerInfo);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditSellerAuthorization(SupplerDto model)
        {
            model.UpdateTime = DateTime.Now;
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var sellerInfo = service.GetSupplerDetail(model.Id);
            sellerInfo.AuthorizationPath = model.AuthorizationPath ?? sellerInfo.AuthorizationPath;

            bool result = service.UpdateSeller(sellerInfo);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductList(int pageIndex, int pageSize, int sellerId)
        {
            List<dynamic> list = new List<dynamic>();
            var service = IocManager.Instance.Resolve<ISuppliersProductService>();
            var productService = IocManager.Instance.Resolve<IProductAppService>();
            var bandService = IocManager.Instance.Resolve<IBrandAppService>();
            var typeService = IocManager.Instance.Resolve<IProductTypeService>();

            var sellerList = service.GetSuppliersProductList(pageIndex, pageSize, sellerId);
            if (sellerList != null)
            {
                foreach (var seller in sellerList)
                {
                    var product = productService.GetProductDetail(seller.ProductId);
                    var band = bandService.GetBrandDetail(product.BrandId);
                    var type = typeService.GetProductTypeDetail(product.TypeId);
                    list.Add(new
                    {
                        Id = seller?.Id,
                        ProductId = product?.ProductId,
                        ProductName = product?.ProductName,
                        TypeName = type?.TypeName,
                        BrandName = band?.BrandName,
                        MarketPrice = product?.MarketPrice,
                        VIPPrice = product?.VIPPrice
                    });
                }
            }

            var total = service.GetSuppliersProductCount(sellerId);
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
        /// 
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        public JsonResult GetSellerProductList()
        {
            List<dynamic> list = new List<dynamic>();
            var productService = IocManager.Instance.Resolve<IProductAppService>();

            foreach (var item in productService.GetProductList())
            {
                list.Add(new { ProductId = item.ProductId, ProductName = item.ProductName, ImgPath = item.ProductImage, Description = item.Description });
            }

            JsonResult.Add("products", list);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AddProduct(string productId, int sellerId, float purchasePrice, int alertStock)
        {
            ISuppliersProductService sellerProductSrv = IocManager.Instance.Resolve<ISuppliersProductService>();
            bool isSucess = sellerProductSrv.AddProduct(new SuppliersProductDto { ProductId = productId, SuppliersId = sellerId, PurchasePrice = purchasePrice, AlertStock = alertStock });
            JsonResult.Add("sellerId", sellerId);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductDetail(int id)
        {
            List<dynamic> list = new List<dynamic>();
            var productService = IocManager.Instance.Resolve<IProductAppService>();

            foreach (var item in productService.GetProductList())
            {
                list.Add(new { ProductId = item.ProductId, ProductName = item.ProductName, ImgPath = item.ProductImage, Description = item.Description });
            }

            JsonResult.Add("products", list);

            ISuppliersProductService sellerProductSrv = IocManager.Instance.Resolve<ISuppliersProductService>();
            var product = sellerProductSrv.GetSellerProductById(id);
            JsonResult.Add("Product", product);

            var productInfo = productService.GetProductDetail(product.ProductId);
            JsonResult.Add("MarketPrice", productInfo.MarketPrice);
            JsonResult.Add("VIPPrice", productInfo.VIPPrice);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditProduct(int id, string productId, float purchasePrice, int alertStock, decimal marketPrice, decimal vipPrice)
        {
            List<dynamic> list = new List<dynamic>();
            var productService = IocManager.Instance.Resolve<IProductAppService>();
            var sellerProductSrv = IocManager.Instance.Resolve<ISuppliersProductService>();
            var sellerProduct = sellerProductSrv.GetSellerProductById(id);
            var product = productService.GetProductDetail(productId);
            product.MarketPrice = marketPrice;
            product.VIPPrice = vipPrice;
            productService.EditProduct(product);
            sellerProduct.ProductId = product.ProductId;
            sellerProduct.AlertStock = alertStock;
            sellerProduct.PurchasePrice = purchasePrice;
            sellerProductSrv.UpdateProduct(sellerProduct);

            JsonResult.Add("sellerId", sellerProduct.SuppliersId);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DelProduct(int id)
        {
            var sellerProductSrv = IocManager.Instance.Resolve<ISuppliersProductService>();
            var result = sellerProductSrv.DelProduct(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SearchSellerByName(string name, int pageIndex, int pageSize)
        {
            var service = IocManager.Instance.Resolve<ISupplerAppService>();
            List<dynamic> list = new List<dynamic>();
            var result = service.SearchSellerByName(name, pageIndex, pageSize);
            foreach (var item in result)
            {
                var acccountSrv = IocManager.Instance.Resolve<IAccountAppService>();
                var accountInfo = acccountSrv.GetAccountInfo(item?.AccountId);
                list.Add(new
                {
                    Id = item.Id,
                    SuppliersName = item?.SuppliersName ?? string.Empty,
                    AccountName = accountInfo?.Fullname ?? string.Empty,
                    State = item?.Status == 0 ? "营业" : "停业"
                });
            }

            var total = service.SearchSellerByNameCount(name);
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

        public JsonResult SearchProductByName(string name, int sellerId, int pageIndex, int pageSize)
        {
            List<dynamic> list = new List<dynamic>();
            var service = IocManager.Instance.Resolve<ISuppliersProductService>();
            var productService = IocManager.Instance.Resolve<IProductAppService>();
            var bandService = IocManager.Instance.Resolve<IBrandAppService>();
            var typeService = IocManager.Instance.Resolve<IProductTypeService>();

            var sellerList = service.SearchProductByName(name, sellerId, pageIndex, pageSize);
            if (sellerList != null)
            {
                foreach (var seller in sellerList)
                {
                    var product = productService.GetProductDetail(seller.ProductId);
                    var band = bandService.GetBrandDetail(product.BrandId);
                    var type = typeService.GetProductTypeDetail(product.TypeId);
                    list.Add(new
                    {
                        Id = seller?.Id,
                        ProductId = product?.ProductId,
                        ProductName = product?.ProductName,
                        TypeName = type?.TypeName,
                        BrandName = band?.BrandName,
                        MarketPrice = product?.MarketPrice,
                        VIPPrice = product?.VIPPrice
                    });
                }
            }

            var total = service.GetProductByNameCount(name, sellerId);
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
