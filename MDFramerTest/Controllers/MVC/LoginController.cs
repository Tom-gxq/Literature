using MD.Core.DomainModel;
using MD.Services.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MD.Web.Controllers.MVC
{
    public class LoginController : Controller
    {
        private readonly IUserService _registerService;
        public LoginController(IUserService registerService)
        {
            this._registerService = registerService;
        }

        [HttpGet]
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Account account)
        {
            string errMessage = string.Empty;
            if (account != null)
            {
                try
                {
                    this._registerService.UserLogin(account);
                }
                catch (ApplicationException ex)
                {
                    errMessage = ex.ToString();
                }
            }
            else
            {
                errMessage = "没有提交登陆信息";
            }
            if(string.IsNullOrEmpty(errMessage))
            {
                Account temp = this._registerService.GetUserAccount(account.Email);
                if (temp != null)
                {
                    Session.Add("account", temp);
                    return Redirect("/UserDetail/Create/?accountId=" + temp.Id);
                }
            }
            else
            {
                ViewBag.ErrorMessage = errMessage;
                return RedirectToAction("Index");
            }
            return View();
        }

        
    }
}
