using AgentDashboard.Models;
using LibMain.Dependency;
using SP.Application.Order;
using SP.Application.Product;
using System;
using System.Collections.Generic;
using System.Configuration;
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
            IOrderAppService service = IocManager.Instance.Resolve<IOrderAppService>();
            var list = service.GetOrderList(0, 1, 20);
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
            return View(list);
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