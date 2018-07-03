using AgentDashboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgentDashboard.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            SPEntities sp = new SPEntities();
            var shopList = sp.SP_Shop.ToList();
            List<ShopViewModel> shopsVM = shopList.Select(x => new ShopViewModel
            {
                Id = x.Id,
                ShopName = x.ShopName,
                ShopType = x.ShopType,
                ShopStatus = x.ShopStatus
            }).ToList();

            return View(shopsVM);
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