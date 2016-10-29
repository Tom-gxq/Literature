using MD.Services.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MD.Web.Controllers
{
    public class DemoController : Controller
    {
        private readonly IDemoService  _demoService;

        public DemoController(IDemoService demoService )
        {
            this._demoService = demoService;
        }
        //
        // GET: /Demo/
        public ActionResult Index()
        {
            string ret = this._demoService.ExcueteService();
            ViewBag.Name = ret;
            return View();
        }

        public ActionResult Details()
        {
            MD.Core.DomainModel.Demo demo = this._demoService.GetDemoById(1);
            ViewBag.Demo = demo;
            return View(demo);
        }

	}
}