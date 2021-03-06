﻿using AccountGRPCInterface;
using AgentDashboard.Models;
using ChartJs.Models;
using LibMain.Dependency;
using ProductGRPCInterface;
using SP.Api.Cache;
using SP.Api.Model.Account;
using SP.Api.Model.Product;
using SP.Application.Chart;
using SP.Application.Order;
using SP.Application.Product;
using SP.Application.Product.DTO;
using SP.Application.Shop;
using SP.Application.Shop.DTO;
using SP.Application.Suppler;
using SP.Application.Suppler.DTO;
using SP.Application.User;
using SP.Application.User.DTO;
using StockGRPCInterface;
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
        #region "帐户"

        /// <summary>
        /// 取得当前用户ID
        /// </summary>
        /// <remarks>
        /// 系统登录后在Session的Account里保存一个AccoutModel
        /// 帐户Id就从这个AccoutModel中取得
        /// </remarks>
        /// <returns>当前用户ID</returns>
        private String GetCurrentAccountId()
        {
            return (MDSession.Session["Account"] as AccountModel)?.AccountId;
        }

        #endregion "帐户"

        public ActionResult DeliverymanViewer(string accountId)
        {
            DeliverymanViewerViewModel viewModel = new DeliverymanViewerViewModel();

            using (SPEntities sPEntities = new SPEntities())
            {
                var act = sPEntities.SP_Account.SingleOrDefault(n => n.AccountId == accountId);
                var actInfo = sPEntities.SP_AccountInfo.SingleOrDefault(n => n.AccountId == accountId);
                //var shopOwner = sPEntities.SP_ShopOwner.SingleOrDefault(n => n.OwnerId == accountId );
                //var shop = sPEntities.SP_Shop.SingleOrDefault(n => n.Id == shopOwner.ShopId);
                //var productType = sPEntities.SP_ProductType.SingleOrDefault(n => n.Id == shop.ShopType);
                //var regionAct = sPEntities.SP_RegionAccount.SingleOrDefault(n => n.AccountId == accountId);
                //if (regionAct != null && string.IsNullOrEmpty(regionAct.AccountId))
                //{
                //    var regionData = sPEntities.SP_RegionData.SingleOrDefault(n => n.DataID == regionAct.RegionId);
                //    viewModel.Region = String.Format("{0},{1}", regionData.DataName, productType.TypeName);
                //}
                var list = sPEntities.SP_AccountAddress.Where(x => x.AccountId == accountId).OrderByDescending(x=>x.IsDefault).ToList();
                if(list != null && list.Count() > 0)
                {
                    var address = list[0];
                    string add = string.Empty;
                    int dormid = int.Parse(address.RegionID);
                    var dorm = sPEntities.SP_RegionData.SingleOrDefault(x=>x.DataID == dormid);
                    if(dorm != null)
                    {
                        add = dorm.DataName;
                        int buildingId = int.Parse(dorm.ParentDataID);
                        var building = sPEntities.SP_RegionData.SingleOrDefault(x => x.DataID == buildingId);
                        if(building != null)
                        {
                            add = building.DataName + " "+ add;
                            int distictId = int.Parse(building.ParentDataID);
                            var distict = sPEntities.SP_RegionData.SingleOrDefault(x => x.DataID == distictId);
                            if(distict != null)
                            {
                                add = distict.DataName + " " + add;
                            }
                        }
                    }
                    viewModel.Dorm = add;
                }
                var finace = sPEntities.SP_AccountFinance.SingleOrDefault(x=>x.AccountId == accountId);
                if(finace != null)
                {
                    var cashList = sPEntities.SP_CashApply.Where(x => x.AccountId == accountId && x.Status == 0).ToList();
                    var sum = cashList.Sum(x=>x.Money) ;
                    viewModel.Amount = (finace.HaveAmount??0) - (finace.UseAmount??0) - (sum!=null?sum.Value:0);
                }
                viewModel.FullName = actInfo?.Fullname;
                viewModel.Birthday = actInfo?.Birthdate != null ? actInfo?.Birthdate.Value.ToShortDateString():string.Empty;
                viewModel.Phone = act?.MobilePhone.Replace("+86","");
            }

            return View(viewModel);
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

        #region "餐厅"

        /// <summary>
        /// 餐厅页面
        /// </summary>
        /// <returns></returns>
        // GET: Default
        public ActionResult Index()
        {
            List<ShopViewModel> shopsVM = null;
            List<RegionViewModel> universities = null;
            List<RegionViewModel> colleges = null;
            using (SPEntities sp = new SPEntities())
            {
                ViewBag.TotalPages = Math.Ceiling((Double)sp.SP_Shop.Count() / 20.00d);
                ViewBag.CurrentPage = 1;
                string food = ConfigurationManager.AppSettings["MainType.Food"];
                int foodId = int.Parse(food);
                String accountId = GetCurrentAccountId();

                universities = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == accountId)
                                join regionData in sp.SP_RegionData on regionAccount.RegionId equals regionData.DataID
                                select new RegionViewModel
                                {
                                    Id = regionData.DataID,
                                    Name = regionData.DataName
                                }).ToList();

                if (universities.Count == 0)
                {
                    return View();
                }

                int universityId = universities[0].Id;

                colleges = (from regionData in sp.SP_RegionData.Where(n => n.ParentDataID == universityId.ToString())
                                   select new RegionViewModel
                                   {
                                       Id = regionData.DataID,
                                       Name = regionData.DataName
                                   }
                                   ).ToList();

                if (colleges.Count() == 0)
                {
                    return View();
                }

                shopsVM = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == accountId && n.RegionId == universityId)
                           join regionData in sp.SP_RegionData on regionAccount.RegionId.ToString() equals regionData.ParentDataID
                           join shop in sp.SP_Shop on regionData.DataID equals shop.RegionId
                           select new ShopViewModel
                           {
                               Id = shop.Id,
                               ShopName = shop.ShopName,
                               ShopType = shop.ShopType,
                               ShopStatus = shop.ShopStatus,
                               RegionId = shop.RegionId,
                           }).Where(n=>n.ShopType == foodId)
                           .OrderByDescending(n =>n.RegionId)
                           .OrderByDescending(n => n.Id).ToList();

                foreach (var item in shopsVM)
                {
                    if (item.RegionId != null)
                    {
                        IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
                        var region = service.GetRegionDataDetail(item.RegionId.Value);
                        item.DistrictName = region?.DataName??string.Empty;
                    }
                }

                ShopsViewModel shopList = new ShopsViewModel() { ShopList = shopsVM, UniversityList = universities, ColleageList = colleges, UniversityId = universityId };

                return View(shopList);
            }
        }

        #endregion "餐厅"

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
                vm.ShopId = shop?.Id??0;
                vm.TypeId = shop?.ShopType??0;
                string market = ConfigurationManager.AppSettings["MainType.Market"];
                int marketId = int.Parse(market);
                if(vm.TypeId == marketId)
                {
                    vm.isMarket = true;
                }
                else
                {
                    vm.isMarket = false;
                }
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
                        deliverMan.Name = account?.Fullname??string.Empty;
                        deliverMan.AccountId = account?.AccountId ?? string.Empty;
                                                
                        if (account != null)
                        {
                            var deliverProducts = sp.SP_AccountProduct.Where(n => n.AccountId == account.AccountId && n.ShopId == shop.Id);
                            foreach (var deliverProduct in deliverProducts.ToList())
                            {
                                ProductsViewModel procduct = new ProductsViewModel();
                                procduct.Id = deliverProduct.Id;
                                procduct.ProductId = deliverProduct.ProductId;
                                DateTime now = DateTime.Parse(DateTime.Now.ToShortDateString());
                                
                                var productStock =  ServerStockBusiness.GetAccountProductStock(account.AccountId, procduct.ProductId, shopId);
                                procduct.Stocks = productStock;
                                procduct.PreStocks = deliverProduct.PreStock;

                                var procdutInfo = sp.SP_Products.SingleOrDefault(n => n.ProductId == procduct.ProductId);
                                procduct.Name = procdutInfo?.ProductName ?? string.Empty;
                                procduct.Description = procdutInfo?.Description;

                                string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
                                var procdutImageInfo = sp.SP_ProductImage.SingleOrDefault(n => n.ProductId == procduct.ProductId);
                                procduct.ImagePath = !string.IsNullOrEmpty(procdutImageInfo?.ImgPath) ? domain + procdutImageInfo.ImgPath : string.Empty;
                                deliverMan.Products.Add(procduct);
                            }
                        }

                        vm.DeliverMen.Add(deliverMan);
                    }
                }
            }

            return View(vm);
        }

        /// <summary>
        /// 超市与餐饮的产品删除
        /// </summary>
        /// <param name="id">ID</param>
        public void DeleteProduct(int id)
        {
            using (SPEntities spEntity = new SPEntities())
            {
                DbContextTransaction transcation = null;

                try
                {
                    var product = spEntity.SP_AccountProduct.SingleOrDefault(n =>n.Id == id);
                    if (product != null)
                    {
                        transcation = spEntity.Database.BeginTransaction();
                        spEntity.SP_AccountProduct.Remove(product);
                        spEntity.SaveChanges();
                        transcation.Commit();
                    }
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

        [HttpPost]
        public JsonResult AddFoodProduct(string accountId,int shopId,string productId,int preStock)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();

            var ret = service.AddFoodProduct(new AccountProductDto()
            {
                AccountId = accountId,
                ProductId = productId,
                ShopId = shopId,
                PreStock = preStock,
                Status = 0,
            });
            JsonResult.Add("status", ret);
            if(ret)
            {
                ServerStockBusiness.AddShopOwnerList(accountId, shopId, productId);
            }
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult AddShopOwner(string accountId, int shopId,int shopType)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            var owner = service.GetShopOwnerByAccountId(accountId);
            if (owner != null)
            {
                JsonResult.Add("status", 1);
            }
            else
            {
                ISupplerAppService sellerService = IocManager.Instance.Resolve<ISupplerAppService>();
                var seller = sellerService.GetSellerDataByAccountId(accountId);
                if (seller != null)
                {
                    JsonResult.Add("status", 2);
                }
                else
                {
                    string market = ConfigurationManager.AppSettings["MainType.Market"];
                    int marketId = int.Parse(market);
                    var entity = new ShopOwnerDto()
                    {
                        OwnerId = accountId,
                        ShopId = shopId,
                        ShopStatus = false

                    };
                    if (marketId != shopType)
                    {
                        entity.ShopStatus = true;
                    }
                    var ret = service.AddShopOwner(entity);
                    if(ret)
                    {
                        //更新用户角色
                        IAccountAppService accountService = IocManager.Instance.Resolve<IAccountAppService>();
                        
                        if (marketId == shopType)
                        {
                            accountService.UpdateAccountUserType(accountId, 3);
                        }
                        else
                        {
                            accountService.UpdateAccountUserType(accountId, 2);
                        }
                        AccountInfoCache.RemoveAccountInfo(accountId);
                    }
                    JsonResult.Add("status", ret?0:-1);
                }
            }

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult DelShopOwner(int shopId,string accountId)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IShopAppService service = IocManager.Instance.Resolve<IShopAppService>();
            AccountInfoCache.RemoveAccountInfo(accountId);
            var ret = service.DelShopOwner(shopId, accountId);
            JsonResult.Add("status", ret ? 0 : -1);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateShopStatus()
        {
            using (SPEntities spEntity = new SPEntities())
            {
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

        /// <summary>
        /// 超市页面
        /// </summary>
        /// <returns></returns>
        public ActionResult SuperMarket()
        {
            List<ShopViewModel> shopsVM = null;
            List<RegionViewModel> universities = null;
            List<RegionViewModel> colleges = null;
            using (SPEntities sp = new SPEntities())
            {
                ViewBag.TotalPages = Math.Ceiling((Double)sp.SP_Shop.Count() / 20.00d);
                ViewBag.CurrentPage = 1;
                string market = ConfigurationManager.AppSettings["MainType.Market"];
                int marketId = int.Parse(market);
                string accountId = GetCurrentAccountId();

                universities = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == accountId)
                                join regionData in sp.SP_RegionData on regionAccount.RegionId equals regionData.DataID
                                select new RegionViewModel
                                {
                                    Id = regionData.DataID,
                                    Name = regionData.DataName
                                }).ToList();

                if (universities.Count == 0)
                {
                    return View();
                }

                int universityId = universities[0].Id;

                colleges = (from regionData in sp.SP_RegionData.Where(n => n.ParentDataID == universityId.ToString())
                            select new RegionViewModel
                            {
                                Id = regionData.DataID,
                                Name = regionData.DataName
                            }).ToList();

                if (colleges.Count() == 0)
                {
                    return View();
                }

                shopsVM = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == accountId && n.RegionId == universityId)
                           join regionData in sp.SP_RegionData on regionAccount.RegionId.ToString() equals regionData.ParentDataID
                           join shop in sp.SP_Shop on regionData.DataID equals shop.RegionId
                           select new ShopViewModel
                           {
                               Id = shop.Id,
                               ShopName = shop.ShopName,
                               ShopType = shop.ShopType,
                               ShopStatus = shop.ShopStatus,
                               RegionId = shop.RegionId,
                           }).Where(n => n.ShopType == marketId)
                           .OrderByDescending(n => n.RegionId)
                           .OrderByDescending(n => n.Id).ToList();

                foreach (var item in shopsVM)
                {
                    if (item.RegionId != null)
                    {
                        IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
                        var region = service.GetRegionDataDetail(item.RegionId.Value);
                        item.DistrictName = region?.DataName ?? string.Empty;
                    }
                }

                ShopsViewModel shopList = new ShopsViewModel() { ShopList = shopsVM, UniversityList = universities, ColleageList = colleges, UniversityId = universityId };

                return View(shopList);
            }
        }

        public ActionResult Details()
        {
            return View();
        }

        public ActionResult HumanManager()
        {
            return View();
        }

        /// <summary>
        /// 取得产品类别列表
        /// </summary>
        /// <remarks>
        /// 人员管理页面
        /// </remarks>
        /// <param name="kind"></param>
        /// <returns></returns>
        public JsonResult GetProductType(int kind)
        {
            using (SPEntities spEntities = new SPEntities())
            {
                var productTypeList = (from productType in spEntities.SP_ProductType
                                       where productType.Kind == kind
                                       select new
                                       {
                                           ID = productType.Id,
                                           Kind = kind,
                                           Name = productType.TypeName
                                       }
                       ).ToList();

                return Json(productTypeList, JsonRequestBehavior.AllowGet);

            }
        }

        /// <summary>
        /// 检索人员管理信息
        /// </summary>
        /// <param name="unversityId"></param>
        /// <param name="colleageId"></param>
        /// <param name="typeId"></param>
        /// <returns></returns>
        public JsonResult GetDeliveryManInfo(int unversityId, int colleageId, int typeId)
        {
            string accountId = GetCurrentAccountId();
            List<HumanManagerViewModel> vmList = null;

            using (SPEntities spEntity = new SPEntities())
            {
                if (unversityId == -1 && colleageId == -1)
                {
                    var queryList = (from regionAccount in spEntity.SP_RegionAccount.Where(n => n.AccountId == accountId)
                                     join unversityRegionData in spEntity.SP_RegionData on regionAccount.RegionId equals unversityRegionData.DataID
                                     join collegeRegionData in spEntity.SP_RegionData on unversityRegionData.DataID.ToString() equals collegeRegionData.ParentDataID
                                     join shop in spEntity.SP_Shop on collegeRegionData.DataID equals shop.RegionId
                                     join shopOwner in spEntity.SP_ShopOwner on shop.Id equals shopOwner.ShopId
                                     join shopProductType in spEntity.SP_ProductType on shop.ShopType equals shopProductType.Id
                                     join account in spEntity.SP_Account on shopOwner.OwnerId equals account.AccountId
                                     join accountInfo in spEntity.SP_AccountInfo on shopOwner.OwnerId equals accountInfo.AccountId
                                     select new HumanManagerViewModel
                                     {
                                         AccountId = account.AccountId,
                                         FullName = accountInfo.Fullname,
                                         CellPhoneNo = account.MobilePhone,
                                         RegionName = collegeRegionData.DataName,
                                         ProductType = shopProductType.Id,
                                         TypeName = shopProductType.TypeName,
                                         CreateTime = account.CreateTime
                                     }).OrderBy(x => x.CreateTime).ToList();

                    if (typeId == -1)
                    {
                        vmList = queryList;
                    }
                    else
                    {
                        vmList = queryList.Where(n=>n.ProductType == typeId).ToList();
                    }
                }
                
                if((unversityId != -1) && (colleageId == -1))
                {
                    var queryList =
                        (from collegeRegionData in spEntity.SP_RegionData.Where(n => n.ParentDataID == unversityId.ToString())
                         join shop in spEntity.SP_Shop on collegeRegionData.DataID equals shop.RegionId
                         join shopOwner in spEntity.SP_ShopOwner on shop.Id equals shopOwner.ShopId
                         join shopProductType in spEntity.SP_ProductType on shop.ShopType equals shopProductType.Id
                         join account in spEntity.SP_Account on shopOwner.OwnerId equals account.AccountId
                         join accountInfo in spEntity.SP_AccountInfo on shopOwner.OwnerId equals accountInfo.AccountId
                         select new HumanManagerViewModel
                         {
                             AccountId = account.AccountId,
                             FullName = accountInfo.Fullname,
                             CellPhoneNo = account.MobilePhone,
                             RegionName = collegeRegionData.DataName,
                             ProductType = shopProductType.Id,
                             TypeName = shopProductType.TypeName,
                             CreateTime = account.CreateTime
                         }).OrderBy(x => x.CreateTime).ToList();

                    if (typeId == -1)
                    {
                        vmList = queryList;
                    }
                    else
                    {
                        vmList = queryList.Where(n => n.ProductType == typeId).ToList();
                    }
                }

                if ((unversityId != -1) && (colleageId != -1))
                {
                    var queryList = (from collegeRegionData in spEntity.SP_RegionData.Where(n => n.DataID == colleageId)
                                     join shop in spEntity.SP_Shop on collegeRegionData.DataID equals shop.RegionId
                                     join shopOwner in spEntity.SP_ShopOwner on shop.Id equals shopOwner.ShopId
                                     join shopProductType in spEntity.SP_ProductType on shop.ShopType equals shopProductType.Id
                                     join account in spEntity.SP_Account on shopOwner.OwnerId equals account.AccountId
                                     join accountInfo in spEntity.SP_AccountInfo on shopOwner.OwnerId equals accountInfo.AccountId
                                     select new HumanManagerViewModel
                                     {
                                         AccountId = account.AccountId,
                                         FullName = accountInfo.Fullname,
                                         CellPhoneNo = account.MobilePhone,
                                         RegionName = collegeRegionData.DataName,
                                         ProductType = shopProductType.Id,
                                         TypeName = shopProductType.TypeName,
                                         CreateTime = account.CreateTime
                                     }).OrderBy(x => x.CreateTime).ToList();

                    if (typeId == -1)
                    {
                        vmList = queryList;
                    }
                    else
                    {
                        vmList = queryList.Where(n => n.ProductType == typeId).ToList();
                    }
                }

                return Json(vmList, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult DataAnalyze()
        {
            string accountId = GetCurrentAccountId();
            using (SPEntities sPEntities = new SPEntities())
            {
                var regionId = sPEntities.SP_RegionAccount.Where(n => n.AccountId == accountId)?.First();
                DataAnalyzeViewModel viewModel = new DataAnalyzeViewModel();
                if (regionId != null)
                {
                    viewModel.RegionId = regionId.RegionId;
                }

                var regionList = (from regionAccount in sPEntities.SP_RegionAccount.Where(n => n.AccountId == accountId)
                                  join regionData in sPEntities.SP_RegionData on regionAccount.RegionId equals regionData.DataID
                                  select new
                                  {
                                      DataId = regionData.DataID,
                                      DataName = regionData.DataName
                                  });

                viewModel.Universities = new Dictionary<int, string>();
                foreach (var region in regionList)
                {
                    viewModel.Universities.Add(region.DataId, region.DataName);
                }

                return View(viewModel);
            }
        }

        public ActionResult ShopManager(int universityId = -1, string productId = "", int sellerId = -1, int type = -1)
        {
            using (SPEntities sp = new SPEntities())
            {
                ShopManagerViewModel viewModel = new ShopManagerViewModel();
                viewModel.Universities = new Dictionary<int, string>();
                viewModel.Sellers = new List<SellerViewModel>();
                viewModel.TypeList = GetProductAllType();
                viewModel.TypeId = type;
                viewModel.ProductId = productId;
                viewModel.ProductName = string.Empty;
                viewModel.SellerId = sellerId;
                viewModel.SellerName = string.Empty;

                string currentAccountId = GetCurrentAccountId();

                foreach (var university in GetUniversityList(currentAccountId))
                {
                    viewModel.Universities.Add(university.Id, university.Name);
                }

                IQueryable<SellerViewModel> sellers = null;
                if (universityId == -1)
                {
                    viewModel.UniversityIdx = -1;
                    sellers = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == currentAccountId)
                               join suppliersRegion in sp.SP_SuppliersRegion on regionAccount.RegionId equals suppliersRegion.RegionID
                               join suppliers in sp.SP_Suppliers on suppliersRegion.SuppliersId equals suppliers.Id
                               select new SellerViewModel
                               {
                                   SellerId = suppliers.Id,
                                   AccountId = currentAccountId,
                                   SellerName = suppliers.SuppliersName,
                                   LogoPath = suppliers.LogoPath,
                                   TypeId = suppliers.TypeId,
                               });
                }
                else
                {
                    sellers = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == currentAccountId)
                               join suppliersRegion in sp.SP_SuppliersRegion.Where(n => n.RegionID == universityId) on regionAccount.RegionId equals suppliersRegion.RegionID
                               join suppliers in sp.SP_Suppliers on suppliersRegion.SuppliersId equals suppliers.Id
                               select new SellerViewModel
                               {
                                   SellerId = suppliers.Id,
                                   AccountId = currentAccountId,
                                   SellerName = suppliers.SuppliersName,
                                   LogoPath = suppliers.LogoPath,
                                   TypeId = suppliers.TypeId,
                               });

                    if(viewModel.Universities.ContainsKey(universityId))
                    {
                        viewModel.UniversityIdx = viewModel.Universities.Keys.ToList().IndexOf(universityId);
                    }
                }

                if (!String.IsNullOrEmpty(productId))
                {
                    viewModel.ProductName = sp.SP_Products.SingleOrDefault(n => n.ProductId == productId)?.ProductName;

                    sellers = (from seller in sellers
                               join product in sp.SP_SuppliersProduct.Where(n => n.ProductId == productId) on seller.SellerId equals product.SuppliersId
                               select new SellerViewModel
                               {
                                   SellerId = seller.SellerId,
                                   AccountId = seller.AccountId,
                                   SellerName = seller.SellerName,
                                   LogoPath = seller.LogoPath,
                                   TypeId = seller.TypeId,
                               });
                }

                if (sellerId != -1)
                {
                    viewModel.SellerName = sp.SP_Suppliers.SingleOrDefault(n => n.Id == sellerId)?.SuppliersName;

                    sellers = sellers.Where(n => n.SellerId == sellerId);
                }

                if (type != -1)
                {
                    sellers = sellers.Where(n => n.TypeId == type);
                }

                viewModel.Sellers = sellers.ToList();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult DelSeller(int id)
        {            
            ISupplerAppService service = IocManager.Instance.Resolve<ISupplerAppService>();
            var detail = service.GetSupplerDetail(id);
            
            var ret = service.DelSuppler(id);
            if(ret && detail != null)
            {
                IAccountAppService accountSvr = IocManager.Instance.Resolve<IAccountAppService>();
                accountSvr.UpdateAccountUserType(detail.AccountId,0);
                AccountInfoCache.RemoveAccountInfo(detail.AccountId);
            }

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
        public JsonResult SearchTypeProductByKeyWord(string keywords,int typeId)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.SearchTypeProductByKeyWord(keywords, typeId,1, 30);
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
            IShopAppService shopService = IocManager.Instance.Resolve<IShopAppService>();

            var owner = shopService.GetShopOwnerByAccountId(model.AccountId);
            if (owner != null)
            {
                JsonResult.Add("status", 1);
            }
            else
            {
                var seller = service.GetSellerDataByAccountId(model.AccountId);
                if (seller != null)
                {
                    JsonResult.Add("status", 2);
                }
                else
                {
                    var ret = service.AddSuppler(new SupplerDto()
                    {
                        AccountId = model.AccountId,
                        AlipayNo = model.AlipayNo,
                        SuppliersName = model.SellerName,
                        LogoPath = model.LogoPath,
                        TelPhone = model.TelNumber
                    });
                    if(ret)
                    {
                        IAccountAppService accountService = IocManager.Instance.Resolve<IAccountAppService>();
                        accountService.UpdateAccountUserType(model.AccountId, 1);
                        AccountInfoCache.RemoveAccountInfo(model.AccountId);
                    }
                    JsonResult.Add("status", ret ? 0 : -1);
                }
            }

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
        public JsonResult SearchOrderListByKeyWord(string keyWord, int pageIndex, int pageSize)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IOrderAppService service = IocManager.Instance.Resolve<IOrderAppService>();
            var list = service.SearchOrderListByKeyWord(keyWord, pageIndex, pageSize);
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
            var total = service.SearchOrderListByKeyWordCount(keyWord);
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
                LogoPath = !string.IsNullOrEmpty(dto.LogoPath) ?domain+dto.LogoPath:string.Empty,
                AlipayNo = dto.AlipayNo,
                AuthorizationPath = !string.IsNullOrEmpty(dto.AuthorizationPath) ? domain + dto.AuthorizationPath : string.Empty,
                LicensePath = !string.IsNullOrEmpty(dto.LicensePath) ? domain + dto.LicensePath : string.Empty,
                PermitPath = !string.IsNullOrEmpty(dto.PermitPath) ? domain + dto.PermitPath : string.Empty,
                TelNumber = dto.TelPhone,
                AccountName = accountInfo?.Fullname??string.Empty,
                TypeList = new List<Models.ProductTypeModel>(),
                ProductDic = new Dictionary<ProductTypeDto, List<ProductsDto>>()
            };
            IProductTypeService typeService = IocManager.Instance.Resolve<IProductTypeService>();
            
            var typeList = new List<ProductTypeDto>();
            if (!string.IsNullOrEmpty(dto.AccountId))
            {
                typeList = typeService.GetTypeList(dto.AccountId, 1, int.MaxValue);
                typeList.ForEach(x=> vm.TypeList.Add(new Models.ProductTypeModel()
                {
                     TypeId = x.TypeId,
                     TypeName = x.TypeName
                }));
                IProductAppService productService = IocManager.Instance.Resolve<IProductAppService>();
                foreach (var item in typeList)
                {
                    var pList = productService.GetSellerProductListByTypeId(dto.AccountId, item.TypeId);
                    foreach(var p in pList)
                    {
                        if(p.ProductImage != null && p.ProductImage.Count > 0)
                        {
                            p.ProductImage[0].ImgPath = !string.IsNullOrEmpty(p.ProductImage[0]?.ImgPath) && !p.ProductImage[0].ImgPath.StartsWith("http://") ? domain + p.ProductImage[0].ImgPath : string.Empty;
                        }
                    }
                    vm.ProductDic.Add(item,pList);
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
        
        public JsonResult GetProductDetail(string productId)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IProductTypeService typeService = IocManager.Instance.Resolve<IProductTypeService>();
            var typeList = typeService.GetAllProductTypeList(0);
            JsonResult.Add("types", typeList);
            var stypeList = typeService.GetAllProductTypeList(1);
            JsonResult.Add("secondTypes", stypeList);
            
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetChildType(int parentTypeId)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            string market = ConfigurationManager.AppSettings["MainType.Market"];
            int marketId = int.Parse(market);
            int kind = 1;
            if(marketId != parentTypeId)
            {
                kind = 2;
            }
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var result = service.GetAllProductTypeList(kind);
            JsonResult.Add("items", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult AddProduct(SellerProductModel product)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var list = ProductBusiness.AddProduct(product);
                JsonResult.Add("status", 0);
            }
            catch (Exception ex)
            {
                JsonResult.Add("status", 1);
            }
            result.Data = JsonResult;
            return result;
        }
        public ActionResult DelProduct(string productId,string suplierId)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            var result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            try
            {
                var product = ProductBusiness.GetSellerProductDetail(productId);
                if(product.suppliersId == suplierId)
                {
                    var list = ProductBusiness.DelProduct(productId);
                    JsonResult.Add("status", 0);
                }
                else
                {
                    JsonResult.Add("status", 1);
                }
            }
            catch (Exception ex)
            {
                JsonResult.Add("status", -1);
            }
            result.Data = JsonResult;
            return result;
        }

        public ActionResult RegionManager()
        {
            string accountId = GetCurrentAccountId();
            using (SPEntities sPEntities = new SPEntities())
            {
                RegionDataViewModel viewModel = new RegionDataViewModel();
                var regionList = (from regionAccount in sPEntities.SP_RegionAccount.Where(n => n.AccountId == accountId)
                                  join regionData in sPEntities.SP_RegionData on regionAccount.RegionId equals regionData.DataID
                                  select new
                                  {
                                      DataId = regionData.DataID,
                                      DataName = regionData.DataName
                                  });

                viewModel.Universities = new Dictionary<int, string>();
                foreach (var region in regionList)
                {
                    viewModel.Universities.Add(region.DataId, region.DataName);
                }

                return View(viewModel);
            }
        }

        public void DeleteRoom(int id)
        {
            using (SPEntities spEntity = new SPEntities())
            {
                DbContextTransaction transcation = null;

                try
                {
                    var room = spEntity.SP_RegionData.SingleOrDefault(n => n.DataID == id);
                    if (room != null)
                    {
                        transcation = spEntity.Database.BeginTransaction();
                        spEntity.SP_RegionData.Remove(room);
                        spEntity.SaveChanges();
                        transcation.Commit();
                    }
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

        public void UpdateRoomName(string name, int dataId)
        {
            using (SPEntities spEntity = new SPEntities())
            {
                DbContextTransaction transcation = null;

                try
                {
                    var room = spEntity.SP_RegionData.SingleOrDefault(n => n.DataID == dataId);
                    if (room != null)
                    {
                        transcation = spEntity.Database.BeginTransaction();
                        room.DataName = name;
                        room.UpdateTime = DateTime.Now;
                        spEntity.SaveChanges();
                        transcation.Commit();
                    }
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

        public void CreateRoom(string name, int dataId, string parentDataId)
        {
            using (SPEntities spEntity = new SPEntities())
            {
                DbContextTransaction transcation = null;

                try
                {
                    if ((dataId == -1) && (!string.IsNullOrEmpty(name) && (name != "?")))
                    {
                        transcation = spEntity.Database.BeginTransaction();
                        spEntity.SP_RegionData.Add(new SP_RegionData() { DataName = name, DataType = 4, Status = 1, ParentDataID= parentDataId, CreateTime = DateTime.Now });
                        spEntity.SaveChanges();
                        transcation.Commit();
                    }
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

        public JsonResult GetUniversity()
        {
            using (SPEntities sp = new SPEntities())
            {
                string accountId = GetCurrentAccountId();
                dynamic universityList = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == accountId)
                                 join regionData in sp.SP_RegionData.Where(n => n.DataType == 1) on regionAccount.RegionId equals regionData.DataID
                                 select new
                                 {
                                     ID = regionData.DataID,
                                     Name = regionData.DataName
                                 }).ToList();

                return Json(universityList, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// 取得学院列表
        /// </summary>
        /// <remarks>
        /// 人员管理页面
        /// </remarks>
        /// <param name="dataId">学校ID</param>
        /// <returns></returns>
        public JsonResult GetColleges(int dataId)
        {
            using (SPEntities spEntity = new SPEntities())
            {
                var collegeList = (from regionData in spEntity.SP_RegionData.Where(n => n.ParentDataID == dataId.ToString())
                                   select new
                                   {
                                       ID = regionData.DataID,
                                       Name = regionData.DataName
                                   }
                                   ).ToList();

                return Json(collegeList, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetBuilding(int dataId)
        {
            List<dynamic> viewModelList = new List<dynamic>();
            using (SPEntities spEntity = new SPEntities())
            {
                var colleges = spEntity.SP_RegionData.Where(n => n.ParentDataID == dataId.ToString())?.ToList();
                foreach (var item in colleges)
                {
                    viewModelList.Add(new { ID = item.DataID, Name = item.DataName });
                }
            }

            return Json(viewModelList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="universityId"></param>
        /// <param name="collegeId"></param>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public JsonResult GetRegionData(int universityId, int collegeId, int buildingId)
        {
            University universityItem = new University();

            using (SPEntities sp = new SPEntities())
            {
                var university = sp.SP_RegionData.SingleOrDefault(n => n.DataID == universityId);
                universityItem.ID = university.DataID;
                universityItem.Name = university.DataName;
                universityItem.Colleges = new List<College>();

                if (collegeId != -1)
                {
                    var college = sp.SP_RegionData.SingleOrDefault(n => n.DataID == collegeId);
                    if (college != null)
                    {
                        College collegeItem= new College() { ID = college.DataID, Name = college.DataName };
                        collegeItem.Buildings = new List<Building>();
                        universityItem.Colleges.Add(collegeItem);

                        if (buildingId != -1)
                        {
                            var building = sp.SP_RegionData.SingleOrDefault(n => n.DataID == buildingId);
                            if (building != null)
                            {
                                Building buildingItem = new Building() { ID = building.DataID, Name = building.DataName };
                                buildingItem.Rooms = new List<Room>();
                                collegeItem.Buildings.Add(buildingItem);
                                var rooms = sp.SP_RegionData.Where(n => n.ParentDataID == building.DataID.ToString());
                                if (rooms != null)
                                {
                                    foreach (var room in rooms)
                                    {
                                        DateTime thirtyDayAgo = DateTime.Now.AddDays(-30);
                                        bool isDanger = (sp.SP_ShipStatistics.Count(n => n.DormId == room.DataID && n.CreateTime >= thirtyDayAgo) == 0);
                                        Room roomItem = new Room() { ID = room.DataID, Name = room.DataName, IsDanger= isDanger };
                                        buildingItem.Rooms.Add(roomItem);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var buildings = sp.SP_RegionData.Where(n => n.ParentDataID == collegeId.ToString());
                            if (buildings != null)
                            {
                                collegeItem.Buildings = new List<Building>();

                                foreach (var building in buildings)
                                {
                                    Building buildingItem = new Building() { ID = building.DataID, Name = building.DataName };
                                    buildingItem.Rooms = new List<Room>();
                                    collegeItem.Buildings.Add(buildingItem);
                                    var rooms = sp.SP_RegionData.Where(n => n.ParentDataID == building.DataID.ToString());
                                    if (rooms != null)
                                    {
                                        foreach (var room in rooms)
                                        {
                                            DateTime thirtyDayAgo = DateTime.Now.AddDays(-30);
                                            bool isDanger = (sp.SP_ShipStatistics.Count(n => n.DormId == room.DataID && n.CreateTime >= thirtyDayAgo) == 0);
                                            Room roomItem = new Room() { ID = room.DataID, Name = room.DataName, IsDanger = isDanger };
                                            buildingItem.Rooms.Add(roomItem);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    var colleges = sp.SP_RegionData.Where(n => n.ParentDataID == universityId.ToString());
                    if (colleges != null)
                    {
                        foreach (var college in colleges)
                        {
                            College collegeItem = new College() { ID = college.DataID, Name = college.DataName };
                            collegeItem.Buildings = new List<Building>();
                            universityItem.Colleges.Add(collegeItem);

                            var buildings = sp.SP_RegionData.Where(n => n.ParentDataID == college.DataID.ToString());
                            if(buildings != null)
                            {
                                foreach (var build in buildings)
                                {
                                    Building buildingItem = new Building() { ID=build.DataID, Name=build.DataName };
                                    buildingItem.Rooms = new List<Room>();
                                    collegeItem.Buildings.Add(buildingItem);
                                    var rooms = sp.SP_RegionData.Where(n => n.ParentDataID == build.DataID.ToString());
                                    if(rooms!= null)
                                    {
                                        foreach (var room in rooms)
                                        {
                                            DateTime thirtyDayAgo = DateTime.Now.AddDays(-30);
                                            bool isDanger = (sp.SP_ShipStatistics.Count(n => n.DormId == room.DataID && n.CreateTime >= thirtyDayAgo) == 0);
                                            Room roomItem = new Room() { ID = room.DataID, Name = room.DataName, IsDanger = isDanger };
                                            buildingItem.Rooms.Add(roomItem);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return Json(universityItem, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderManager()
        {
            string accountId = GetCurrentAccountId();
            using (SPEntities sPEntities = new SPEntities())
            {
                OrderManagerViewModel viewModel = new OrderManagerViewModel();
                var regionList = (from regionAccount in sPEntities.SP_RegionAccount.Where(n => n.AccountId == accountId)
                                  join regionData in sPEntities.SP_RegionData on regionAccount.RegionId equals regionData.DataID
                                  select new
                                  {
                                      DataId = regionData.DataID,
                                      DataName = regionData.DataName
                                  });

                viewModel.Universities = new Dictionary<int, string>();
                foreach (var region in regionList)
                {
                    viewModel.Universities.Add(region.DataId, region.DataName);
                }
                return View(viewModel);
            }
        }

        public JsonResult GetOrderList(int universityId, int status, int pageIndex, int pageSize)
        {
            using (SPEntities sPEntities = new SPEntities())
            {
                List<OrderManagerModel> ordersList;
                if (status == -1)
                {
                    ordersList = sPEntities.SP_Orders.Where(n => n.RegionId == universityId).Select(x => new OrderManagerModel
                    {
                        OrderId = x.OrderId,
                        OrderCode = x.OrderCode,
                        OrderDate = x.OrderDate,
                        OrderStatus = x.OrderStatus,
                        OrderAddress = x.OrderAddress ?? string.Empty,
                        AccountId = x.AccountId,
                        IsVip = x.IsVip,
                        Amount = x.Amount,
                        IsWxPay = x.IsWxPay,
                        IsAliPay = x.IsAliPay,
                        Remark = x.Remark ?? string.Empty
                    }).ToList();
                }
                else
                {
                    ordersList = sPEntities.SP_Orders.Where(n => n.RegionId == universityId && n.OrderStatus == status).Select(x => new OrderManagerModel
                    {
                        OrderId = x.OrderId,
                        OrderCode = x.OrderCode,
                        OrderDate = x.OrderDate,
                        OrderStatus = x.OrderStatus,
                        OrderAddress = x.OrderAddress ?? string.Empty,
                        AccountId = x.AccountId,
                        IsVip = x.IsVip,
                        Amount = x.Amount,
                        IsWxPay = x.IsWxPay,
                        IsAliPay = x.IsAliPay,
                        Remark = x.Remark ?? string.Empty
                    }).ToList();
                }

                var list = ordersList.Skip(pageIndex * pageSize).Take(pageSize).ToList();

                foreach (var item in list)
                {
                    item.Owner = new AccountInfoDto();
                    item.Owner.Fullname = sPEntities.SP_AccountInfo.SingleOrDefault(n=>n.AccountId == item.AccountId)?.Fullname;
                    item.Owner.Mobile = sPEntities.SP_Account.SingleOrDefault(n => n.AccountId == item.AccountId)?.MobilePhone;

                    var shipOrderList = sPEntities.SP_ShippingOrders.Where(n => n.OrderId == item.OrderId)?.GroupBy(n => n.ShippingId);

                    item.Shiper = new List<AccountInfoDto>();

                    foreach (var shipOrder in shipOrderList)
                    {
                        var shiper = new AccountInfoDto();
                        shiper.AccountId = shipOrder.Key;
                        shiper.Fullname = sPEntities.SP_AccountInfo.SingleOrDefault(n => n.AccountId == shipOrder.Key)?.Fullname;
                        shiper.Mobile = sPEntities.SP_Account.SingleOrDefault(n => n.AccountId == shipOrder.Key)?.MobilePhone?.Replace("+86", "") ?? string.Empty;
                        item.Shiper.Add(shiper);
                    }
                }

                Dictionary<string, object> JsonResult = new Dictionary<string, object>();
                var total = ordersList.Count;
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
                AccountModel accountModel = null;
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
                        accountModel = AccountInfoCache.GetAccountInfoByAccountId(accountEntity.AccountId);
                        MDSession.Session["Account"] = accountModel;
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

        /// <summary>
        /// 数据分析页面之用户注册数
        /// </summary>
        /// <param name="regionId">学校Id</param>
        /// <param name="day">天数</param>
        /// <returns></returns>
        public JsonResult GetUserNameLineChartData(int regionId, int day)
        {
            Chart chart = new Chart();
            List<string> dayList = new List<string>();
            
            for (int i = day - 1; i > 0; i--)
            {
                double dDay = -i;
                dayList.Add(DateTime.Now.AddDays(dDay).ToString("yyyy/MM/dd"));
            }

            dayList.Add(DateTime.Now.ToString("yyyy/MM/dd"));

            chart.labels = dayList.ToArray();

            List<int> userCntList = new List<int>();
            using (SPEntities spEntity = new SPEntities())
            {
                foreach (var strDay in dayList)
                {
                    DateTime dateTime = DateTime.Parse(strDay);
                    int? newUserCnt = spEntity.SP_SysStatistics.Where(n=>n.RegionId == regionId).SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_NewUser;
                    userCntList.Add(newUserCnt ?? 0);
                }
            }

            List<Datasets> dataSet = new List<Datasets>();
            dataSet.Add(new Datasets()
            {
                label = DateTime.Now.ToString("yyyy"),
                data = userCntList.ToArray(),
                borderColor = new string[] { "#800080" },
                borderWidth = "1"
            });

            chart.datasets = dataSet;
            return Json(chart, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public JsonResult GetNewOrderChartData(int regionId, int day)
        {
            Chart chart = new Chart();
            List<string> dayList = new List<string>();

            for (int i = day - 1; i > 0; i--)
            {
                double dDay = -i;
                dayList.Add(DateTime.Now.AddDays(dDay).ToString("yyyy/MM/dd"));
            }

            dayList.Add(DateTime.Now.ToString("yyyy/MM/dd"));

            chart.labels = dayList.ToArray();

            List<int> newOderCnt = new List<int>();
            using (SPEntities spEntity = new SPEntities())
            {
                foreach (var strDay in dayList)
                {
                    DateTime dateTime = DateTime.Parse(strDay);
                    int? newOrder = spEntity.SP_SysStatistics.Where(n => n.RegionId == regionId).SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_NewOrder;
                    newOderCnt.Add(newOrder ?? 0);
                }
            }

            List<Datasets> dataSet = new List<Datasets>();
            dataSet.Add(new Datasets()
            {
                label = DateTime.Now.ToString("yyyy"),
                data = newOderCnt.ToArray(),
                borderColor = new string[] { "#800080" },
                borderWidth = "1"
            });

            chart.datasets = dataSet;
            return Json(chart, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public JsonResult GetNewAssociatorChartData(int regionId, int day)
        {
            Chart chart = new Chart();
            List<string> dayList = new List<string>();

            for (int i = day - 1; i > 0; i--)
            {
                double dDay = -i;
                dayList.Add(DateTime.Now.AddDays(dDay).ToString("yyyy/MM/dd"));
            }

            dayList.Add(DateTime.Now.ToString("yyyy/MM/dd"));

            chart.labels = dayList.ToArray();

            List<int> newAssociatorCnt = new List<int>();
            using (SPEntities spEntity = new SPEntities())
            {
                foreach (var strDay in dayList)
                {
                    DateTime dateTime = DateTime.Parse(strDay);
                    int? newAssociator = spEntity.SP_SysStatistics.Where(n => n.RegionId == regionId).SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_NewAssociator;
                    newAssociatorCnt.Add(newAssociator ?? 0);
                }
            }

            List<Datasets> dataSet = new List<Datasets>();
            dataSet.Add(new Datasets()
            {
                label = DateTime.Now.ToString("yyyy"),
                data = newAssociatorCnt.ToArray(),
                borderColor = new string[] { "#800080" },
                borderWidth = "1"
            });

            chart.datasets = dataSet;
            return Json(chart, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public JsonResult GetOrderAmountChartData(int regionId, int day)
        {
            Chart chart = new Chart();
            List<string> dayList = new List<string>();

            for (int i = day - 1; i > 0; i--)
            {
                double dDay = -i;
                dayList.Add(DateTime.Now.AddDays(dDay).ToString("yyyy/MM/dd"));
            }

            dayList.Add(DateTime.Now.ToString("yyyy/MM/dd"));

            chart.labels = dayList.ToArray();

            List<int> orderAmountList = new List<int>();
            List<int> markAmountList = new List<int>();
            using (SPEntities spEntity = new SPEntities())
            {
                foreach (var strDay in dayList)
                {
                    DateTime dateTime = DateTime.Parse(strDay);
                    decimal? orderAmount = spEntity.SP_SysStatistics.Where(n => n.RegionId == regionId).SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_FoodOrderAmount;
                    orderAmountList.Add((int)(orderAmount ?? 0));

                    decimal? markAmount = spEntity.SP_SysStatistics.SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_MarkOrderAmount;
                    markAmountList.Add((int)(markAmount ?? 0));
                }
            }            

            List<Datasets> dataSet = new List<Datasets>();
            dataSet.Add(new Datasets()
            {
                label = "餐饮",
                backgroundColor=new string[] { "#800080" },
                data = orderAmountList.ToArray(),
                borderColor = new string[] { "#800080" },
                borderWidth = "1",
                fill=false
            });
            dataSet.Add(new Datasets()
            {
                label = "超市",
                backgroundColor = new string[] { "#00FF00" },
                data = markAmountList.ToArray(),
                borderColor = new string[] { "#00FF00" },
                borderWidth = "1",
                fill = false
            });

            chart.datasets = dataSet;
            return Json(chart, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSellerLineChartData(int day,int sellerId,int type)
        {
            Chart chart = new Chart();
            List<string> dayList = new List<string>();

            for (int i = day - 1; i > 0; i--)
            {
                double dDay = -i;
                dayList.Add(DateTime.Now.AddDays(dDay).ToString("yyyy/MM/dd"));
            }

            dayList.Add(DateTime.Now.ToString("yyyy/MM/dd"));

            chart.labels = dayList.ToArray();

            List<int> userCntList = new List<int>();
            ISellerStatisticsAppService service = IocManager.Instance.Resolve<ISellerStatisticsAppService>();
            
            foreach (var strDay in dayList)
            {
                DateTime dateTime = DateTime.Parse(strDay);
                var entity = service.GetSellerStatistics(sellerId, dateTime);
                if (type == 1)
                {
                    userCntList.Add(entity?.NewOrder??0);
                }
                else
                {
                    userCntList.Add(entity != null ?((int)entity.OrderAmount):0);
                }
            }

            List<Datasets> dataSet = new List<Datasets>();
            dataSet.Add(new Datasets()
            {
                label = DateTime.Now.ToString("yyyy"),
                data = userCntList.ToArray(),
                borderColor = new string[] { "#800080" },
                borderWidth = "1"
            });

            chart.datasets = dataSet;
            return Json(chart, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateStock(string productId,string accountId, int shopId, int stock)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            var list = new List<ProductSkuModel>();
            list.Add(new ProductSkuModel()
            {
                accountId = accountId,
                productId = productId,
                shopId = shopId,
                stock = stock,
                type = 0,//覆盖
            });
            var ret = ServerStockBusiness.UpdateProductSku(list);

            JsonResult.Add("status", ret?0:1);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UpdatePreStock(string productId, string accountId, int shopId,int stock)
        {
            Dictionary<string, object> JsonResult = new Dictionary<string, object>();
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var ret = service.UpdateAccountPreStock(new AccountProductDto()
            {
                 AccountId = accountId,
                 ProductId = productId,
                 ShopId = shopId,
                 PreStock = stock
            });
            JsonResult.Add("status", ret?0:1);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public void UpdateProductMarketPrice(string id, int price)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var productInfo = service.GetProductDetail(id);
            productInfo.MarketPrice = price;
            service.EditProduct(productInfo);
        }

        public void UpdateProductVIPPrice(string id, int price)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var productInfo = service.GetProductDetail(id);
            productInfo.VIPPrice = price;
            service.EditProduct(productInfo);
        }

        public JsonResult GetShopListByRegionId(int unversityId, int collegeId)
        {
            dynamic shopsVM;
            using (SPEntities sp = new SPEntities())
            {
                ViewBag.TotalPages = Math.Ceiling((Double)sp.SP_Shop.Count() / 20.00d);
                ViewBag.CurrentPage = 1;
                string food = ConfigurationManager.AppSettings["MainType.Food"];
                int foodId = int.Parse(food);
                String accountId = GetCurrentAccountId();

                if (collegeId == -1)
                {

                    shopsVM = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == accountId && n.RegionId == unversityId)
                               join regionData in sp.SP_RegionData on regionAccount.RegionId.ToString() equals regionData.ParentDataID
                               join shop in sp.SP_Shop on regionData.DataID equals shop.RegionId
                               select new ShopViewModel
                               {
                                   Id = shop.Id,
                                   ShopName = shop.ShopName,
                                   ShopType = shop.ShopType,
                                   ShopStatus = shop.ShopStatus,
                                   RegionId = shop.RegionId,
                               }).Where(n => n.ShopType == foodId)
                               .OrderByDescending(n => n.RegionId)
                               .OrderByDescending(n => n.Id).ToList();
                }
                else
                {
                    shopsVM = (from regionData in sp.SP_RegionData.Where(n => n.DataID == collegeId)
                               join shop in sp.SP_Shop on regionData.DataID equals shop.RegionId
                               select new ShopViewModel
                               {
                                   Id = shop.Id,
                                   ShopName = shop.ShopName,
                                   ShopType = shop.ShopType,
                                   ShopStatus = shop.ShopStatus,
                                   RegionId = shop.RegionId,
                               }).Where(n => n.ShopType == foodId)
                               .OrderByDescending(n => n.RegionId)
                               .OrderByDescending(n => n.Id).ToList();
                }

                foreach (var item in shopsVM)
                {
                    if (item.RegionId != null)
                    {
                        IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
                        var region = service.GetRegionDataDetail(item.RegionId);
                        item.DistrictName = region?.DataName ?? string.Empty;
                    }
                }
            }

            var result = Json(shopsVM, JsonRequestBehavior.AllowGet);

            return result;
        }

        public JsonResult GetSupperMarketListByRegionId(int unversityId, int collegeId)
        {
            dynamic shopsVM;
            using (SPEntities sp = new SPEntities())
            {
                ViewBag.TotalPages = Math.Ceiling((Double)sp.SP_Shop.Count() / 20.00d);
                ViewBag.CurrentPage = 1;
                string market = ConfigurationManager.AppSettings["MainType.Market"];
                int marketId = int.Parse(market);
                String accountId = GetCurrentAccountId();

                if (collegeId == -1)
                {

                    shopsVM = (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == accountId && n.RegionId == unversityId)
                               join regionData in sp.SP_RegionData on regionAccount.RegionId.ToString() equals regionData.ParentDataID
                               join shop in sp.SP_Shop on regionData.DataID equals shop.RegionId
                               select new ShopViewModel
                               {
                                   Id = shop.Id,
                                   ShopName = shop.ShopName,
                                   ShopType = shop.ShopType,
                                   ShopStatus = shop.ShopStatus,
                                   RegionId = shop.RegionId,
                               }).Where(n => n.ShopType == marketId)
                               .OrderByDescending(n => n.RegionId)
                               .OrderByDescending(n => n.Id).ToList();
                }
                else
                {
                    shopsVM = (from regionData in sp.SP_RegionData.Where(n => n.DataID == collegeId)
                               join shop in sp.SP_Shop on regionData.DataID equals shop.RegionId
                               select new ShopViewModel
                               {
                                   Id = shop.Id,
                                   ShopName = shop.ShopName,
                                   ShopType = shop.ShopType,
                                   ShopStatus = shop.ShopStatus,
                                   RegionId = shop.RegionId,
                               }).Where(n => n.ShopType == marketId)
                               .OrderByDescending(n => n.RegionId)
                               .OrderByDescending(n => n.Id).ToList();
                }

                foreach (var item in shopsVM)
                {
                    if (item.RegionId != null)
                    {
                        IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
                        var region = service.GetRegionDataDetail(item.RegionId);
                        item.DistrictName = region?.DataName ?? string.Empty;
                    }
                }
            }

            var result = Json(shopsVM, JsonRequestBehavior.AllowGet);

            return result;
        }

        #region "产品"

        /// <summary>
        /// 产品所有类别列表
        /// </summary>
        /// <returns>所有类别列表</returns>
        private Dictionary<int, string> GetProductAllType()
        {
            Dictionary<int, string> returnValue = new Dictionary<int, string>();

            using (SPEntities sp = new SPEntities())
            {
                foreach (var productType in sp.SP_ProductType)
                {
                    returnValue.Add(productType.Id, productType.TypeName);
                }

                return returnValue;
            }
        }

        #endregion "产品"

        #region "区域"

        /// <summary>
        /// 根据帐户Id取得学校列表
        /// </summary>
        /// <param name="accountId">帐户Id</param>
        /// <returns>学校列表</returns>
        private List<RegionViewModel> GetUniversityList(string accountId)
        {
            using (SPEntities sp = new SPEntities())
            {
                return (from regionAccount in sp.SP_RegionAccount.Where(n => n.AccountId == accountId)
                        join regionData in sp.SP_RegionData on regionAccount.RegionId equals regionData.DataID
                        select new RegionViewModel
                        {
                            Id = regionData.DataID,
                            Name = regionData.DataName
                        }).ToList();
            }
        }

        /// <summary>
        /// 根据学校Id取得院区列表
        /// </summary>
        /// <param name="unversityId">学校Id</param>
        /// <returns>院区列表</returns>
        private List<RegionViewModel> GetCollegeList(int unversityId)
        {
            using (SPEntities sp = new SPEntities())
            {
                return (from regionData in sp.SP_RegionData.Where(n => n.ParentDataID == unversityId.ToString())
                        select new RegionViewModel
                        {
                            Id = regionData.DataID,
                            Name = regionData.DataName
                        }).ToList();
            }
        }

        #endregion "区域"
    }
}