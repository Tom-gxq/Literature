using MD.Core.DomainModel;
using MD.Services.Register;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MD.Web.Controllers.MVC
{
    public class UserDetailController : Controller
    {
        private readonly IUserService _userService;
        public UserDetailController(IUserService user)
        {
            this._userService = user;
        }
        // GET: UserDetail/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserDetail/Create
        public ActionResult Create(int accountId)
        {
            UserInfo user = this._userService.GetUserInfo(accountId);
            if(user != null)
            {
                return Redirect("/UserDetail/Edit?id=" + accountId);
            }
            return View();
        }

        // POST: UserDetail/Create
        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            try
            {
                // TODO: Add insert logic here
                Account account = Session["account"] as Account;
                userInfo.Account = account;
                this._userService.AddUserInfo(userInfo);
            }
            catch
            {
               
            }
            return Redirect("/UserDetail/Edit?id=" + userInfo.Account.Id);
        }

        // GET: UserDetail/Edit/5
        public ActionResult Edit(int id)
        {
            UserInfo user = null;
            try
            {
                user = this._userService.GetUserInfo(id);
            }
            catch
            {

            }
            return View(user);
        }

        
        
    }
}
