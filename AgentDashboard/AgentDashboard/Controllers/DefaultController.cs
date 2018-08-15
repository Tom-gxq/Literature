using AccountGRPCInterface;
using AgentDashboard.Models;
using ChartJs.Models;
using LibMain.Dependency;
using ProductGRPCInterface;
using SP.Api.Cache;
using SP.Api.Model.Account;
using SP.Api.Model.Product;
using SP.Application.Order;
using SP.Application.Product;
using SP.Application.Product.DTO;
using SP.Application.Shop;
using SP.Application.Shop.DTO;
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
                string food = ConfigurationManager.AppSettings["MainType.Food"];
                int foodId = int.Parse(food);

                var shopList = sp.SP_Shop.Where(n=>n.ShopType == foodId)?.OrderByDescending(n => n.RegionId).OrderByDescending(n=>n.Id)
                    //.Skip(GetStartRowNo(1, 20)).Take(20).ToList();
                    .ToList();
                shopsVM = shopList.Select(x => new ShopViewModel
                {
                    Id = x.Id,
                    ShopName = x.ShopName,
                    ShopType = x.ShopType,
                    ShopStatus = x.ShopStatus,
                    RegionId = x.RegionId,
                }).ToList();
                foreach(var item in shopsVM)
                {
                    if (item.RegionId != null)
                    {
                        IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
                        var region = service.GetRegionDataDetail(item.RegionId.Value);
                        item.DistrictName = region.DataName;
                    }
                }
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
                        deliverMan.Name = account.Fullname;
                        deliverMan.AccountId = account.AccountId;

                        

                        var deliverProducts = sp.SP_AccountProduct.Where(n => n.AccountId == account.AccountId && n.ShopId == shop.Id);

                        foreach (var deliverProduct in deliverProducts)
                        {
                            ProductsViewModel procduct = new ProductsViewModel();
                            procduct.Id = deliverProduct.ProductId;
                            DateTime now = DateTime.Parse(DateTime.Now.ToShortDateString());
                            var productStock = sp.SP_ProductSKUs.SingleOrDefault(n => n.ProductId == procduct.Id 
                            && n.AccountId == account.AccountId && n.ShopId == shopId 
                            && n.EffectiveTime >= now);
                            if (productStock != null)
                            {
                                procduct.Stocks = productStock.Stock;
                                procduct.PreStocks = deliverProduct.PreStock;
                            }

                            var procdutInfo = sp.SP_Products.SingleOrDefault(n => n.ProductId == procduct.Id);
                            procduct.Name = procdutInfo?.ProductName??string.Empty;
                            procduct.Description = procdutInfo?.Description;

                            string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
                            var procdutImageInfo = sp.SP_ProductImage.SingleOrDefault(n => n.ProductId == procduct.Id);
                            procduct.ImagePath = !string.IsNullOrEmpty(procdutImageInfo?.ImgPath)? domain+ procdutImageInfo.ImgPath : string.Empty;
                            deliverMan.Products.Add(procduct);
                        }

                        vm.DeliverMen.Add(deliverMan);
                    }
                }
            }

            return View(vm);
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
                    var ret = service.AddShopOwner(new ShopOwnerDto()
                    {
                        OwnerId = accountId,
                        ShopId = shopId,
                    });
                    if(ret)
                    {
                        //更新用户角色
                        IAccountAppService accountService = IocManager.Instance.Resolve<IAccountAppService>();
                        string market = ConfigurationManager.AppSettings["MainType.Market"];
                        int marketId = int.Parse(market);
                        if (marketId == shopType)
                        {
                            accountService.UpdateAccountUserType(accountId, 3);
                        }
                        else
                        {
                            accountService.UpdateAccountUserType(accountId, 2);
                        }
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
            List<ShopViewModel> shopsVM = null;
            using (SPEntities sp = new SPEntities())
            {
                ViewBag.TotalPages = Math.Ceiling((Double)sp.SP_Shop.Count() / 20.00d);
                ViewBag.CurrentPage = 1;
                string market = ConfigurationManager.AppSettings["MainType.Market"];
                int marketId = int.Parse(market);
                
                var shopList = sp.SP_Shop.Where(n => n.ShopType == marketId)?.OrderByDescending(n => n.RegionId).OrderByDescending(n => n.Id)
                    //.Skip(GetStartRowNo(1, 20)).Take(20).ToList();
                    .ToList();
                shopsVM = shopList.Select(x => new ShopViewModel
                {
                    Id = x.Id,
                    ShopName = x.ShopName,
                    ShopType = x.ShopType,
                    ShopStatus = x.ShopStatus,
                    RegionId = x.RegionId,
                }).ToList();
                foreach (var item in shopsVM)
                {
                    if (item.RegionId != null)
                    {
                        IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
                        var region = service.GetRegionDataDetail(item.RegionId.Value);
                        item.DistrictName = region.DataName;
                    }
                }
            }

            return View(shopsVM);
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
                    }
                    JsonResult.Add("status", ret);
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
                ProductDic = new List<List<ProductsDto>>()
            };
            IProductTypeService typeService = IocManager.Instance.Resolve<IProductTypeService>();
            
            var typeList = new List<ProductTypeDto>();
            if (!string.IsNullOrEmpty(dto.AccountId))
            {
                typeList = typeService.GetTypeList(dto.AccountId, 1, 30);
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

        public ActionResult RegionManager()
        {
            RegionDataViewModel viewModel = new RegionDataViewModel();

            using (SPEntities sp = new SPEntities())
            {
                var regionList = sp.SP_RegionData.Where(n => n.DataType == 1).ToList();
                viewModel.Universities = new Dictionary<int, string>();

                foreach (var region in regionList)
                {
                    viewModel.Universities.Add(region.DataID, region.DataName);
                }
            }

            return View(viewModel);
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

        public JsonResult GetColleges(int dataId)
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

        class RegionData
        {
            public List<University> Universities;
        }

        class University
        {
            public int ID;
            public string Name;
            public List<College> Colleges;
        };

        class College
        {
            public int ID;
            public string Name;
            public List<Building> Buildings;
        }

        class Building
        {
            public int ID;
            public string Name;
            public List<Room> Rooms;
        }

        class Room
        {
            public int ID;
            public string Name;
            public string ParentDataID;
            public int DataType;
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
                                        Room roomItem = new Room() { ID = room.DataID, Name = room.DataName };
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
                                            Room roomItem = new Room() { ID=room.DataID, Name=room.DataName };
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
                                            Room roomItem = new Room() { ID=room.DataID, Name=room.DataName };
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
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public JsonResult GetUserNameLineChartData(int day)
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
                    int? newUserCnt = spEntity.SP_SysStatistics.SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_NewUser;
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
        public JsonResult GetNewOrderChartData(int day)
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
                    int? newOrder = spEntity.SP_SysStatistics.SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_NewOrder;
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
        public JsonResult GetNewAssociatorChartData(int day)
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
                    int? newAssociator = spEntity.SP_SysStatistics.SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_NewAssociator;
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
        public JsonResult GetOrderAmountChartData(int day)
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
            using (SPEntities spEntity = new SPEntities())
            {
                foreach (var strDay in dayList)
                {
                    DateTime dateTime = DateTime.Parse(strDay);
                    int? orderAmount = spEntity.SP_SysStatistics.SingleOrDefault(n => n.CreateTime.Year == dateTime.Year && n.CreateTime.Month == dateTime.Month && n.CreateTime.Day == dateTime.Day)?.Num_NewAssociator;
                    orderAmountList.Add(orderAmount ?? 0);
                }
            }

            List<Datasets> dataSet = new List<Datasets>();
            dataSet.Add(new Datasets()
            {
                label = DateTime.Now.ToString("yyyy"),
                data = orderAmountList.ToArray(),
                borderColor = new string[] { "#800080" },
                borderWidth = "1"
            });

            chart.datasets = dataSet;
            return Json(chart, JsonRequestBehavior.AllowGet);
        }
    }
}