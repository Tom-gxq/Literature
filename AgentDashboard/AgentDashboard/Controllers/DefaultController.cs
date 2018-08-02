﻿using AccountGRPCInterface;
using AgentDashboard.Models;
using LibMain.Dependency;
using SP.Api.Cache;
using SP.Api.Model.Account;
using SP.Application.Order;
using SP.Application.Product;
using SP.Application.Product.DTO;
using SP.Application.Suppler;
using SP.Application.Suppler.DTO;
using SP.Application.User;
using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApiGateway.App_Start.Crypt;

namespace AgentDashboard.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult DeliverymanViewer()
        {
            return View();
        }

        public ActionResult ShopManagerDemo()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private int GetStartRowNo(int pageIndex, int pageSize)
        {
            if ((pageIndex <= 0) || (pageSize <= 0))
            {
                return 0;
            }

            return (pageIndex - 1) * pageSize;
        }

        // GET: Default
        public ActionResult Index()
        {
            List<ShopViewModel> shopsVM = null;
            using (SPEntities sp = new SPEntities())
            {
                ViewBag.TotalPages = Math.Ceiling((Double)sp.SP_Shop.Count() / 20.00d);
                ViewBag.CurrentPage = 1;

                var shopList = sp.SP_Shop.OrderByDescending(n => n.RegionId).OrderByDescending(n=>n.Id)
                    //.Skip(GetStartRowNo(1, 20)).Take(20).ToList();
                    .ToList();
                shopsVM = shopList.Select(x => new ShopViewModel
                {
                    Id = x.Id,
                    ShopName = x.ShopName,
                    ShopType = x.ShopType,
                    ShopStatus = x.ShopStatus
                }).ToList();
            }

            return View(shopsVM);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shopId">店铺Id</param>
        /// <returns></returns>
        public ActionResult ShopDetails(int shopId)
        {
            ShopDetailsViewModel vm = new ShopDetailsViewModel();
            using (SPEntities sp = new SPEntities())
            {
                var shop = sp.SP_Shop.SingleOrDefault(n => n.Id == shopId);
                vm.ShopName = shop?.ShopName;
                DateTime startTime;
                bool isSuccess = DateTime.TryParse(shop?.StartTime, out startTime);
                if (isSuccess)
                {
                    vm.StartTime = startTime;
                }
                else
                {
                    vm.StartTime = new DateTime(0);
                }
                DateTime endTime;
                isSuccess = DateTime.TryParse(shop?.EndTime, out endTime);
                if (isSuccess)
                {
                    vm.EndTime = endTime;
                }
                else
                {
                    vm.EndTime = new DateTime(0);
                }

                var shopOwnerList = sp.SP_ShopOwner.Where(n => n.ShopId == shopId);

                if (shopOwnerList != null)
                {
                    vm.DeliverManCount = shopOwnerList.Count();
                    vm.DeliverMen = new List<DeliverManViewModel>();

                    foreach (var shopOwner in shopOwnerList)
                    {
                        var deliverMan = new DeliverManViewModel();
                        deliverMan.Products = new List<ProductsViewModel>();

                        var account = sp.SP_AccountInfo.SingleOrDefault(n => n.AccountId == shopOwner.OwnerId);
                        deliverMan.Name = account.Fullname;

                        ProductsViewModel procduct = new ProductsViewModel();

                        var deliverProducts = sp.SP_AccountProduct.Where(n => n.AccountId == account.AccountId && n.ShopId == shop.Id);

                        foreach (var deliverProduct in deliverProducts)
                        {
                            procduct.Id = deliverProduct.ProductId;
                            var productStock = sp.SP_ProductSKUs.SingleOrDefault(n => n.ProductId == procduct.Id);
                            procduct.Stocks = productStock.Stock;
                            procduct.PreStocks = deliverProduct.PreStock;

                            var procdutInfo = sp.SP_Products.SingleOrDefault(n => n.ProductId == procduct.Id);
                            procduct.Name = procdutInfo.ProductName;
                            procduct.Description = procdutInfo?.Description;

                            string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
                            var procdutImageInfo = sp.SP_ProductImage.SingleOrDefault(n => n.ProductId == procduct.Id);
                            procduct.ImagePath = !string.IsNullOrEmpty(procduct?.ImagePath)? domain+ procduct.ImagePath:string.Empty;
                            deliverMan.Products.Add(procduct);
                        }

                        vm.DeliverMen.Add(deliverMan);
                    }
                }
            }

            return View(vm);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateShopStatus()
        {
            SPEntities spEntity = new SPEntities();
            var sr = new StreamReader(Request.InputStream);
            var stream = sr.ReadToEnd();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var list = js.Deserialize<List<ShopViewModel>>(stream);

            if (list != null)
            {
                foreach (var item in list)
                {
                    DbContextTransaction transcation = null;

                    try
                    {
                        var shop = spEntity.SP_Shop.Where(n => n.Id == item.Id).FirstOrDefault();

                        transcation = spEntity.Database.BeginTransaction();
                        shop.ShopStatus = item.ShopStatus;
                        spEntity.SaveChanges();
                        transcation.Commit();
                    }
                    catch (Exception)
                    {
                        transcation.Rollback();
                        throw;
                    }
                    finally
                    {
                        if (transcation != null) transcation.Dispose();
                    }
                }
            }
            
            return Json(String.Empty);
        }

        // GET: Default
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult SuperMarket()
        {
            return View();
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult HumanManager()
        {
            try
            {
                using (SPEntities spEntity = new SPEntities())
                {
                    //spEntity.SP_Account
                }
            }
            catch (Exception)
            {
            }
            return View();
        }

        public ActionResult DataAnalyze()
        {
            return View();
        }

        public ActionResult ShopManager()
        {
            List<SellerViewModel> shopsVM = null;
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var list = service.GetSupplerList();
            
            shopsVM = list.Select(x => new SellerViewModel
            {
                SellerId = x.Id,
                AccountId = x.AccountId,
                SellerName = x.SuppliersName,
                LogoPath = x.LogoPath,
            }).ToList();

            return View(shopsVM);
        }
        [HttpPost]
        public ActionResult DelSeller(int id)
        {            
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var list = service.DelSuppler(id);

            return Json(String.Empty);
        }
        public JsonResult SearchProductByKeyWord(string keywords)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.SearchProductByKeyWord(keywords, 1, 30);
            JsonResult.Add("items", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SearchSellerData(string name)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var result = service.SearchSellerData(name);
            JsonResult.Add("items", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult SearchSeller(string productId="",int sellerId=0,int type=-1)
        {
            List<SellerViewModel> shopsVM = null;
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var list = service.SearchSuppler(productId, sellerId, type,1,20);

            shopsVM = list.Select(x => new SellerViewModel
            {
                SellerId = x.Id,
                AccountId = x.AccountId,
                SellerName = x.SuppliersName,
                LogoPath = x.LogoPath
            }).ToList();

            return View(shopsVM);
        }
        public JsonResult AddSeller(SellerViewModel model )
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            
            var ret = service.AddSuppler(new SupplerDto()
            {
                 AccountId = model.AccountId,
                 AlipayNo = model.AlipayNo,
                 SuppliersName = model.SellerName,
                 LogoPath = model.LogoPath,
                 TelPhone = model.TelNumber
            });
            JsonResult.Add("status", ret);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SearchAccountByKeyWord(string keywords)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IAccountAppService service = IocManager.Instance.Resolve<IAccountAppService>();
            var result = service.SearchAccount(keywords);
            JsonResult.Add("items", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult UpdateSeller(SellerViewModel model)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var ret = service.UpdateSeller(new SupplerDto()
            {
                Id = model.SellerId,
                AlipayNo = model.AlipayNo,
                SuppliersName = model.SellerName,
                LogoPath = model.LogoPath,
                TelPhone = model.TelNumber,
                AuthorizationPath = model.AuthorizationPath,
                LicensePath = model.LicensePath,
                PermitPath = model.PermitPath,                
            });
            JsonResult.Add("status", ret);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult SellerDetails(int sellerId)
        {
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var dto = service.GetSupplerDetail(sellerId);
            
            AccountInfoDto accountInfo = null;
            if (!string.IsNullOrEmpty(dto?.AccountId))
            {
                IAccountAppService accountService = IocManager.Instance.Resolve<IAccountAppService>();
                accountInfo = accountService.GetAccountInfo(dto.AccountId);
            }
            
            string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
            var vm = new SellerViewModel
            {
                SellerId = dto.Id,
                AccountId = dto.AccountId,
                SellerName = dto.SuppliersName,
                LogoPath = dto.LogoPath,//!string.IsNullOrEmpty(dto.LogoPath) ?domain+dto.LogoPath:string.Empty,
                AlipayNo = dto.AlipayNo,
                AuthorizationPath = dto.AuthorizationPath,//!string.IsNullOrEmpty(dto.AuthorizationPath) ? domain + dto.AuthorizationPath : string.Empty,
                LicensePath = dto.LicensePath,//!string.IsNullOrEmpty(dto.LicensePath) ? domain + dto.LicensePath : string.Empty,
                PermitPath = dto.PermitPath,//!string.IsNullOrEmpty(dto.PermitPath) ? domain + dto.PermitPath : string.Empty,
                TelNumber = dto.TelPhone,
                AccountName = accountInfo?.Fullname??string.Empty,
                TypeList = new List<ProductTypeModel>(),
                ProductDic = new List<List<ProductsDto>>()
            };
            IProductTypeService typeService = IocManager.Instance.Resolve<IProductTypeService>();
            var account = MDSession.Session["Account"] as AccountModel;
            var typeList = new List<ProductTypeDto>();
            if (account != null)
            {
                typeList = typeService.GetTypeList(account.AccountId, 1, 30);
                typeList.ForEach(x=> vm.TypeList.Add(new ProductTypeModel()
                {
                     TypeId = x.TypeId,
                     TypeName = x.TypeName
                }));
                IProductAppService productService = IocManager.Instance.Resolve<IProductAppService>();
                foreach (var item in typeList)
                {
                    var pList = productService.GetSellerProductListByTypeId(account.AccountId, item.TypeId);
                    foreach(var p in pList)
                    {
                        if(p.ProductImage != null && p.ProductImage.Count > 0)
                        {
                            p.ProductImage[0].ImgPath = !string.IsNullOrEmpty(p.ProductImage[0]?.ImgPath) ? domain + p.ProductImage[0].ImgPath : string.Empty;
                        }
                    }
                    vm.ProductDic.Add(pList);
                }
            }

            
            return View(vm);
        }
        public JsonResult GetProductTypeList()
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var account = MDSession.Session["Account"] as AccountInfo;
            if (account != null)
            {
                var result = service.GetTypeList(account.AccountId, 1, 30);
                JsonResult.Add("result", result);
            }
            
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult RegionManager()
        {
            return View();
        }

        public ActionResult OrderManager()
        {
            return View();
        }
        public JsonResult GetOrderList(int status, int pageIndex, int pageSize)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IOrderAppService service = IocManager.Instance.Resolve<IOrderAppService>();
            var list = service.GetOrderList(status, pageIndex, pageSize);
            var total = service.GetOrderListCount(status);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);

            IProductAppService productService = IocManager.Instance.Resolve<IProductAppService>();
            foreach (var item in list)
            {
                item.ProductList = productService.GetProductListByOrderId(item.OrderId);
                foreach (var pitem in item.ProductList)
                {
                    string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
                    foreach (var pimg in pitem.ProductImage)
                    {
                        if (!string.IsNullOrEmpty(pimg.ImgPath) && !string.IsNullOrEmpty(domain))
                        {
                            pimg.ImgPath = domain + pimg.ImgPath;
                        }
                    }
                }
            }
            JsonResult.Add("result", list);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult CreateAccount(AccountViewModel vm)
        {
            return RedirectToAction("Index");
        }

        public ActionResult CheckLogin(AccountViewModel vm)
        {
            //必须字段
            if (!string.IsNullOrEmpty(vm.UserName))
            {
                var account = CheckAccount(vm.UserName);
                //加密密码
                var password = StringCrypt.Encrypt(vm.Password, ConfigInfo.ConfigInfoData.CryptKey.MessageKey);
                var accountEntity = AccountBusiness.GetAccount(account);
                if (accountEntity == null)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    if (accountEntity.Password != password)
                    {
                        return RedirectToAction("Login"); //密码错误
                    }
                    else
                    {
                       
                        MDSession.Session.Clear();
                        MDSession.Session["Account"] = AccountInfoCache.GetAccountInfoByAccountId(accountEntity.AccountId);
                    }
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }
        private static string CheckAccount(string account)
        {
            if (!ValidateAccount(account))
                return string.Empty;

            if (!account.StartsWith("86") && !account.StartsWith("+"))
                account = "86" + account;

            if (!account.Contains("+"))
                account = "+" + account;

            return account;
        }
        private static bool ValidateAccount(string account)
        {
            if (string.IsNullOrWhiteSpace(account))
                return false;

            if (account.Length < 5 || (!ValidateEmail(account) && !Regex.IsMatch(account, @"^[+]?\d+$")))
                return false;

            return true;
        }
        private static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
            return Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)*\.[\w-]+$");
        }

    }
}