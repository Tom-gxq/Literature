using AgentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgentDashboard.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult HumanManagerDemo()
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

                var shopList = sp.SP_Shop.OrderByDescending(n => n.RegionId).OrderByDescending(n=>n.Id).Skip(GetStartRowNo(1, 20)).Take(20).ToList();
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
            return View();
        }

        public ActionResult DataAnalyze()
        {
            return View();
        }

        public ActionResult ShopManager()
        {
            return View();
        }

        public ActionResult RegionManager()
        {
            return View();
        }

        public ActionResult OrderManager()
        {
            return View();
        }

        public ActionResult CreateAccount(AccountViewModel vm)
        {
            return RedirectToAction("Index");
        }

        public ActionResult CheckLogin(AccountViewModel vm)
        {
            return RedirectToAction("Index");
        }

    }
}